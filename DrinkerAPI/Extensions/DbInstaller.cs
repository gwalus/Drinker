using DrinkerAPI.Data;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrinkerAPI.Extensions
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoctailContext>(options => options.UseSqlite("Data source = coctaildb.db").UseLazyLoadingProxies());
            services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddRoles<IdentityRole<int>>()
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
              .AddEntityFrameworkStores<CoctailContext>();
        }
    }
}
