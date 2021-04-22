using DrinkerAPI.Data;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
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

        public CoctailRepository(CoctailContext context)
        {
            _context = context;
        }

        public async Task<Coctail> GetCoctailByNameAsync(string keyword)
        {
            return await _context.Coctails.Where(x => x.Name.Equals(keyword)).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Coctail>> GetCoctailsByIngredientsAsync(IList<string> ingredients, CoctailParams coctailParams)
        {
            var coctailsQueryable = _context.Coctails.AsQueryable();

            var query = QueryBuilder.BuildIngredientsQuery(coctailsQueryable, ingredients);

            query = QueryBuilder.AddFiltersQuery(query, coctailParams);

            return await PagedList<Coctail>.CreateAsync(query, coctailParams.PageNumber, coctailParams.PageSize);
        }

        public async Task<PagedList<Coctail>> GetListOfCoctailsAsync(PaginationParams paginationParams)
        {
            var query = _context.Coctails.AsQueryable();

            return await PagedList<Coctail>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
