using AutoMapper;
using AutoMapper.QueryableExtensions;
using DrinkerAPI.Data;
using DrinkerAPI.Dtos;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
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

        public CoctailRepository(CoctailContext context, IMapper mapper,ICloudinaryService cloudinary)
        {
            _context = context;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<CoctailDto> GetCoctailDtoByIdAsync(int Id)
        {
            return await _context.Coctails
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .Where(x => x.Id == Id)
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

        public async Task<bool> AddCoctail(CoctailToAdd coctailToAdd)
        {
            var coctail = new Coctail
            {
                Alcoholic = coctailToAdd.Alcoholic,
                Category = coctailToAdd.Category,
                DateModified = DateTime.Now.ToString(),
                Glass = coctailToAdd.Glass,
                Ingradients = coctailToAdd.Ingradients,
                Name = coctailToAdd.Name,
                Instructions = coctailToAdd.Instructions,
                PhotoUrl = await _cloudinary.UploadFile(coctailToAdd.PhotoUrl),
                IsAccepted = false,

            };
            if (coctail.PhotoUrl == null)
            {
                return false;
            }
            coctail.IsAccepted = false;
            await _context.Coctails.AddAsync(coctail);
            var added = await _context.SaveChangesAsync();
            return added > 0;
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
    }
}
