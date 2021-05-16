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

        public CoctailRepository(CoctailContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoctailDto> GetCoctailByIdAsync(int Id)
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

        public async Task<PagedList<CoctailDto>> GetCoctailsByIngredientsAsync(IList<string> ingredients, CoctailParams coctailParams)
        {
            var coctailsQueryable = _context.Coctails
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            var query = QueryBuilder.BuildIngredientsQuery(coctailsQueryable, ingredients);

            query = QueryBuilder.AddFiltersQuery(query, coctailParams);

            return await PagedList<CoctailDto>.CreateAsync(query, coctailParams.PageNumber, coctailParams.PageSize);
        }

        public async Task<IList<string>> GetCoctailNamesAsync()
        {
            return await _context.Coctails.Select(c => c.Name).ToListAsync();
        }

        public async Task<PagedList<CoctailDto>> GetListOfCoctailsAsync(PaginationParams paginationParams)
        {
            var query = _context.Coctails
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
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // QUERY FOR POSTRESS
            //var postgresquery = _context.Coctails
            //    .OrderBy(r => Guid.NewGuid())
            //    .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
            //    .Take(count)
            //    .ToListAsync();

            //return await PagedList<CoctailDto>.CreateAsync(postgresquery, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<bool> AddCoctail(Coctail coctail)
        {
            coctail.IsAccepted = false;
            await _context.AddAsync(coctail);
            var added = await _context.SaveChangesAsync();
            return added > 0;
        }
    }
}
