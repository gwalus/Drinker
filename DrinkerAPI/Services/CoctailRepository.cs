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

        public async Task<Coctail> GetCoctailByName(string keyword)
        {
            return await _context.Coctails.Where(x => x.Name.Equals(keyword)).FirstOrDefaultAsync();
        }
        public async Task<IList<Coctail>> GetCoctailsByIngredientsAsync(IList<string> ingredients)
        {
            var coctailsQueryable = _context.Coctails.AsQueryable();

            var query = QueryBuilder.BuildIngredientsQuery(coctailsQueryable, ingredients);

            return await query.ToListAsync();
        }

        public async Task<ICollection<Coctail>> GetListOfCoctailsAsync()
        {
            return await _context.Coctails.ToListAsync();
        }
    }
}
