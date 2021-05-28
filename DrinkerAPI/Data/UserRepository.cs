using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DrinkerAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly CoctailContext _coctailContext;

        public UserRepository(CoctailContext coctailContext)
        {
            _coctailContext = coctailContext;
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await _coctailContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
