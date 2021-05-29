using AutoMapper;
using AutoMapper.QueryableExtensions;
using DrinkerAPI.Data;
using DrinkerAPI.Dtos;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Services
{
    /// <summary>
    /// inheridoc
    /// </summary>
    public class CoctailRepository : ICoctailRepository
    {
        private readonly CoctailContext _context;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinary;

        public CoctailRepository(CoctailContext context, IMapper mapper, ICloudinaryService cloudinary)
        {
            _context = context;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<CoctailDto> GetCoctailDtoByIdAsync(int Id)
        {
            return await _context.Coctails
                .Include(x=>x.Ingradients)
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .Where(y => y.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<CoctailDto>> GetCoctailsByNameAsync(string keyword)
        {
            // SQLITE DOESN'T SUPPORT CONTAINS WITH STRINGCOMPARISON.IGNORECASE
            return await _context.Coctails
                .Where(x => x.IsAccepted)
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .Where(x => EF.Functions.Like(x.Name, $"%{keyword}%"))
                .ToListAsync();
        }

        public async Task<List<string>> GetCoctailCategories()
        {
            return await _context.Coctails
                .Select(c => c.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<string>> GetCoctailGlasses()
        {
            return await _context.Coctails
                .Select(c => c.Glass)
                .Distinct()
                .ToListAsync();
        }

        public async Task<PagedList<CoctailDto>> GetCoctailsAsync(string name, IList<string> ingredients, CoctailParams coctailParams)
        {
            var coctailsQueryable = _context.Coctails
                .Where(x => x.IsAccepted)
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                coctailsQueryable = coctailsQueryable.Where(x => EF.Functions.Like(x.Name, $"%{name}%"));
            }

            var query = QueryBuilder.BuildIngredientsQuery(coctailsQueryable, ingredients);

            query = QueryBuilder.AddFiltersQuery(query, coctailParams);

            return await PagedList<CoctailDto>.CreateAsync(query, coctailParams.PageNumber, coctailParams.PageSize);
        }

        public async Task<IList<string>> GetCoctailNamesAsync()
        {
            return await _context.Coctails
                .Where(c => c.IsAccepted)
                .Select(c => c.Name)
                .ToListAsync();
        }

        public async Task<PagedList<CoctailDto>> GetCoctailsToAccept(PaginationParams paginationParams)
        {
            var query = _context.Coctails
                .Where(x => !x.IsAccepted)
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<CoctailDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<List<CoctailDto>> GetRandomCoctailsAsync(int count)
        {
            count = Math.Abs(count) > 8 ? 8 : count;

            // SQLITE DOESN'T SUPPORT ORDERING BY NEW GUID
            return await _context.Coctails
                .FromSqlRaw($"SELECT * FROM Coctails ORDER BY RANDOM() LIMIT {count}")
                .Where(x => x.IsAccepted)
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // QUERY FOR POSTRESS
            //var postgresquery = _context.Coctails
            //    .Where(r => r.IsAccepted)
            //    .OrderBy(r => Guid.NewGuid())
            //    .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
            //    .Take(count)
            //    .ToListAsync();

            //return await PagedList<CoctailDto>.CreateAsync(postgresquery, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<int> AddCoctail(CoctailToAdd coctailToAdd, int userId)
        {
            var coctail = new Coctail
            {
                Alcoholic = coctailToAdd.Alcoholic,
                Category = coctailToAdd.Category,
                DateModified = DateTime.Now.ToString(),
                Glass = coctailToAdd.Glass,
                Name = coctailToAdd.Name,
                Instructions = coctailToAdd.Instructions,
                IsAccepted = false,
                Ingradients = coctailToAdd.Ingradients.Select(x => new Ingredient { Name = x.Name, Measure = x.Measure }).ToList(),
                UserId = userId
            };

            await _context.Coctails.AddAsync(coctail);
            if(await _context.SaveChangesAsync() > 0)    
                return coctail.Id;

            return 0;
        }

        public async Task<bool> AcceptCoctail(int Id)
        {
            var coctail = await _context.Coctails.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (coctail != null)
            {
                coctail.IsAccepted = true;
                var accepted = await _context.SaveChangesAsync();
                return accepted > 0;
            }
            return false;
        }

        public async Task<bool> RejectCoctail(int Id)
        {
            var coctail = await _context.Coctails.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (coctail != null && coctail.IsAccepted == false)
            {
                _context.Coctails.Remove(coctail);
                var rejected = await _context.SaveChangesAsync();
                return rejected > 0;
            }
            return false;
        }

        public async Task<IList<string>> GetIngredientNamesAsync()
        {
            return await _context.Ingredients.Select(x => x.Name).Distinct().ToListAsync();
        }

        public async Task<bool> AddCocktailToFavourite(FavouriteCoctail favouriteCoctail)
        {
            await _context.FavouriteCoctails.AddAsync(favouriteCoctail);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Coctail> GetCoctailByIdAsync(int id)
        {
            return await _context.Coctails
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<CoctailDto>> GetFavouritesCocktails(int userId, PaginationParams paginationParams)
        {
            var favouritedCocktails = _context.FavouriteCoctails
                 .Where(fc => fc.AppUserId == userId)
                 .Include(fc => fc.Coctail)
                 .Select(fc => fc.Coctail)
                 .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                 .AsQueryable();

            return await PagedList<CoctailDto>.CreateAsync(favouritedCocktails, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<bool> DeleteFromFavouritesAsync(FavouriteCoctail favouriteCoctail)
        {
            _context.FavouriteCoctails.Remove(favouriteCoctail);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<FavouriteCoctail> GetFavouriteCoctailAsync(int userId, int cocktailId)
        {
            return await _context.FavouriteCoctails.SingleOrDefaultAsync(fc => fc.AppUserId == userId && fc.CoctailId == cocktailId);
        }

        public async Task<bool> IsCocktailFavouriteAsync(int userId, int cocktailId)
        {
            return await _context.FavouriteCoctails.AnyAsync(fc => fc.AppUserId == userId && fc.CoctailId == cocktailId);
        }

        public async Task<bool> AddPhotoToCocktail(IFormFile photo, int cocktailId)
        {
            var cocktail = await _context.Coctails.SingleOrDefaultAsync(x => x.Id == cocktailId);

            if (cocktail != null)
            {
                var publicPhotoUrl = await _cloudinary.UploadFile(photo);

                if (!string.IsNullOrEmpty(publicPhotoUrl))
                {
                    cocktail.PhotoUrl = publicPhotoUrl;
                    return await _context.SaveChangesAsync() > 0;                  
                }
            }

            return false;
        }
    }
}
