using DrinkerAPI.Data;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Services
{
    public class CoctailRepository : ICoctailRepository
    {
        private CoctailContext _context;

        public CoctailRepository(CoctailContext context)
        {
            _context = context;
        }

        public async Task<Coctail> GetCoctailByName(string keyword)
        {
            return await _context.Coctails.Where(x => x.Name.Equals(keyword)).FirstOrDefaultAsync();
        }

        public async Task<IList<Coctail>> GetCoctailsByIngredient(string keyword)
        {
            var ingredient = _context.Ingredients.Where(x => x.Name
            .Equals(keyword))
                .First();
            if (ingredient == null)
                return null;
            else
                return await _context.Coctails.Include(i => i.Ingradients.Where(x => x.Name == keyword)).ToListAsync();
        }

        public async Task<ICollection<Coctail>> GetListOfCoctailsAsync()
        {
            return await _context.Coctails.ToListAsync();
        }

    }

}
