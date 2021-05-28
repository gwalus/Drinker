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

            builder.Entity<Coctail>()
                .HasMany(x => x.Ingradients)
                .WithOne(e => e.Coctail)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Ingredient>()
                .HasOne(x => x.Coctail)
                .WithMany(e => e.Ingradients);

            builder.Entity<FavouriteCoctail>()
                .HasKey(k => new { k.AppUserId, k.CoctailId});

            builder.Entity<FavouriteCoctail>()
                .HasOne(s => s.AppUser)
                .WithMany(l => l.FavouriteCoctails)
                .HasForeignKey(s => s.AppUserId);

            builder.Entity<FavouriteCoctail>()
                .HasOne(s => s.Coctail)
                .WithMany(l => l.FavouritedByUsers)
                .HasForeignKey(s => s.CoctailId);
        }
    }
}
