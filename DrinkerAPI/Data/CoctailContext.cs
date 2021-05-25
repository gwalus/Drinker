using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrinkerAPI.Data
{
    // public class CoctailContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int, IdentityUserClaim<int>,
    //  IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

    public class CoctailContext : IdentityDbContext
    {
        public CoctailContext(DbContextOptions<CoctailContext> options) : base(options)
        {
        }

        public DbSet<Coctail> Coctails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
