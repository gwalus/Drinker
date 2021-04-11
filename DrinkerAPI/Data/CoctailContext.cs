using DrinkerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinkerAPI.Data
{
    public class CoctailContext : DbContext
    {
        public CoctailContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coctail> Coctails { get; set; }
        public DbSet<Ingredient> Ingredients{ get; set; }
    }
}
