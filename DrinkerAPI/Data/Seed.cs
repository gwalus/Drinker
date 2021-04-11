﻿using DrinkerAPI.Models;
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
        public static async Task SeedData(CoctailContext context)
        {
            if (await context.Coctails.AnyAsync()) return;

            var coctailsData = await File.ReadAllTextAsync(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "DataDownloader/Coctails.json"));
            var coctails = JsonSerializer.Deserialize<List<Coctail>>(coctailsData);
            if (coctails == null) return;

            foreach (var item in coctails)
            {
                await context.AddAsync(item);
                await context.SaveChangesAsync();
            }
        }
    }
}