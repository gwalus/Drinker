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

        public Task<IList<Coctail>> GetCoctailsByIngredient(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Coctail>> GetListOfCoctailsAsync()
        {
            return await _context.Coctails.ToListAsync();
        }

    }

}
