using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrinkerAPI.Data
{
    public class CoctailContext : IdentityDbContext
    {
        public CoctailContext(DbContextOptions <CoctailContext>options) : base(options)
        {
        }

        public DbSet<Coctail> Coctails { get; set; }
        public DbSet<Ingredient> Ingredients{ get; set; }
    }
}
