using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Extensions
{
    public class BasicReferencesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrinkerAPI", Version = "v1" });
            });
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<ICoctailRepository, CoctailRepository>();
            services.AddControllers();
        
        }
    }
}
