using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrinkerAPI.Data
{
    public class CoctailContext : IdentityDbContext<AppUser, IdentityRole<int>, int, IdentityUserClaim<int>,
     IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public CoctailContext(DbContextOptions<CoctailContext> options) : base(options)
        {
        }

        public DbSet<Coctail> Coctails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<FavouriteCoctail> FavouriteCoctails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FavouriteCoctail>()
                .HasKey(k => new { k.CoctailId, k.UserId });

            builder.Entity<FavouriteCoctail>()
                .HasOne(s => s.User)
                .WithMany(l => l.FavouriteCoctails)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FavouriteCoctail>()
                .HasOne(s => s.Coctail)
                .WithMany(l => l.FavouritedByUsers)
                .HasForeignKey(s => s.CoctailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
