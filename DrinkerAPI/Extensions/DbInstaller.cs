using DrinkerAPI.Data;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DrinkerAPI.Extensions
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoctailContext>(options =>
            {
                //options.UseNpgsql(config.GetConnectionString("DefaultConnection"));

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = configuration.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // Locally on Docker
                    // connStr = configuration.GetConnectionString("DefaultConnection");

                    // Use connection string provided at runtime by Heroku.
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];
                    var updatedHost = pgHost.Replace("flycast", "internal");

                    connStr = $"Server={updatedHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from Heroku, use it to set up your DbContext.
                options.UseNpgsql(connStr);
            });

            services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddRoles<IdentityRole<int>>()
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
              .AddEntityFrameworkStores<CoctailContext>();
        }
    }
}
