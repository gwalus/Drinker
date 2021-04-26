﻿using DrinkerAPI.Data;
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
        }
    }
}
