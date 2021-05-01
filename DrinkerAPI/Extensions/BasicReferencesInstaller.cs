﻿using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Options;
using DrinkerAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrinkerAPI", Version = "v1" });
            });
            //Mapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //Repositories
            services.AddScoped<ICoctailRepository, CoctailRepository>();
            //Jwt
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof( jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                   ValidateAudience = false,
                   RequireExpirationTime = false,
                   ValidateLifetime = true,
                   ValidateIssuer = false, //<----
                    };
           });
            //AddControllers
            services.AddControllers();

        
        }
    }
}
