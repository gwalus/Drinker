using AutoMapper;
using AutoMapper.QueryableExtensions;
using DrinkerAPI.Data;
using DrinkerAPI.Dtos;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CoctailDto> GetCoctailByNameAsync(string keyword)
        {
            return await _context.Coctails
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .Where(x => x.Name.Equals(keyword))
                .FirstOrDefaultAsync();
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

        public async Task<PagedList<CoctailDto>> GetListOfCoctailsAsync(PaginationParams paginationParams)
        {
            var query = _context.Coctails
                .ProjectTo<CoctailDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<CoctailDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
