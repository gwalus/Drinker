using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DrinkerAPI.Data
{
    public class Seed
    {
        /// <summary>Seeds the data.</summary>
        /// <param name="context">The context.</param>
        //public static async Task SeedData(CoctailContext context, UserManager<IdentityUser> userManager)
        public static async Task SeedData(CoctailContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await context.Coctails.AnyAsync()) return;

            var coctailsData = await File.ReadAllTextAsync(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "DataDownloader/Coctails.json"));

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

            //FOR DEVELOPMENT
            var roles = new List<IdentityRole>{
                new IdentityRole{Name="User"},
                new IdentityRole{Name="Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new IdentityUser { UserName = "admin", Email = "admin@gmail.com" };
            var defaultUser = new IdentityUser { UserName = "user", Email = "user@gmail.com" };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.CreateAsync(defaultUser, "Pa$$w0rd");

            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddToRoleAsync(defaultUser, "User");
        }
    }
}
