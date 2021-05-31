using DrinkerAPI.Data;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using DrinkerAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DrinkerAPI.Tests.Integration
{
    public class DependencySetupFixtureForPostgres
    {
        IConfigurationRoot _configurationRoot;

        public DependencySetupFixtureForPostgres()
        {
            _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testsSettings.json")
            .Build();

            var serviceCollection = new ServiceCollection();
            var connStr = _configurationRoot.GetConnectionString("DefaultConnection");

            serviceCollection.AddDbContext<CoctailContext>(options => options.UseNpgsql(connStr));


            serviceCollection.AddScoped<ICoctailRepository, CoctailRepository>();
            serviceCollection.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            serviceCollection.AddScoped<ICloudinaryService, CloudinaryService>();
            serviceCollection.Configure<CloudinarySettings>(_configurationRoot.GetSection("CloudinarySettings"));

            serviceCollection.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddRoles<IdentityRole<int>>()
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
              .AddEntityFrameworkStores<CoctailContext>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}
