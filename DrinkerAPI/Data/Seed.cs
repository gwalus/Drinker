﻿using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace DrinkerAPI.Data
{
    public class Seed
    {
        /// <summary>Seeds the data.</summary>
        /// <param name="context">The context.</param>
        //public static async Task SeedData(CoctailContext context, UserManager<IdentityUser> userManager)
        public static async Task SeedData(CoctailContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (await context.Coctails.AnyAsync()) 
                return;

            // CHANGES FOR FLY.IO DEPLOYMENT            
            var workingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var coctailsData = await File.ReadAllTextAsync(Path.Combine(workingDirectory, "Coctails.json"));

            var options = new JsonSerializerOptions
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };

            var coctails = JsonSerializer.Deserialize<List<Coctail>>(coctailsData, options);
            
            if (coctails == null) return;

            foreach (var item in coctails)
            {
                item.IsAccepted = true;
                await context.AddAsync(item);
                await context.SaveChangesAsync();
            }

            ////FOR DEVELOPMENT
            var roles = new List<IdentityRole<int>>{
                new IdentityRole<int>{Name="User"},
                new IdentityRole<int>{Name="Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new AppUser { UserName = "admin", Email = "admin@gmail.com" };
            var defaultUser = new AppUser { UserName = "user", Email = "user@gmail.com" };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.CreateAsync(defaultUser, "Pa$$w0rd");

            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddToRoleAsync(defaultUser, "User");
        }
    }
}
