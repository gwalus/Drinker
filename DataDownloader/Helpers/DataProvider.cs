﻿using DataDownloader.Models;
using DrinkerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataDownloader.Helpers
{
    public class DataProvider
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<DrinkItem> GetDrinkByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}");

            if (request.Content == null) return null;

            var response = await request.Content.ReadAsStringAsync();

            var drink = JsonSerializer.Deserialize<CoctailResponseModel>(response)
                ?.drinks
                ?.FirstOrDefault();

            if (drink == null) return null;

            return drink;
        }

        public static IList<Ingredient> CreateIngredients(DrinkItem drink)
        {
            IList<string> ingradientNames = new List<string>();
            IList<string> ingradientMeasures = new List<string>();

            ingradientNames.Add(drink.strIngredient1);
            ingradientNames.Add(drink.strIngredient2);
            ingradientNames.Add(drink.strIngredient3);
            ingradientNames.Add(drink.strIngredient4);
            ingradientNames.Add(drink.strIngredient5);
            ingradientNames.Add(drink.strIngredient6);
            ingradientNames.Add(drink.strIngredient7);
            ingradientNames.Add(drink.strIngredient8);
            ingradientNames.Add(drink.strIngredient9);
            ingradientNames.Add(drink.strIngredient10);
            ingradientNames.Add(drink.strIngredient11);
            ingradientNames.Add(drink.strIngredient12);
            ingradientNames.Add(drink.strIngredient13);

            ingradientNames = ingradientNames.Where(ing => ing != null).ToList();

            ingradientMeasures.Add(drink.strMeasure1);
            ingradientMeasures.Add(drink.strMeasure2);
            ingradientMeasures.Add(drink.strMeasure3);
            ingradientMeasures.Add(drink.strMeasure4);
            ingradientMeasures.Add(drink.strMeasure5);
            ingradientMeasures.Add(drink.strMeasure6);
            ingradientMeasures.Add(drink.strMeasure7);
            ingradientMeasures.Add(drink.strMeasure8);
            ingradientMeasures.Add(drink.strMeasure9);
            ingradientMeasures.Add(drink.strMeasure10);
            ingradientMeasures.Add(drink.strMeasure11);
            ingradientMeasures.Add(drink.strMeasure12);
            ingradientMeasures.Add(drink.strMeasure13);

            ingradientMeasures = ingradientMeasures.Where(mea => mea != null).ToList();

            IList<Ingredient> ingredients = new List<Ingredient>();

            for (int i = 0; i < ingradientNames.Count; i++)
            {
                var ingredient = new Ingredient
                {
                    Id = i,
                    Name = ingradientNames.ElementAtOrDefault(i),
                    Measure = ingradientMeasures.ElementAtOrDefault(i)?.TrimEnd()
                };

                ingredients.Add(ingredient);
            }

            return ingredients;
        }
    }
}
