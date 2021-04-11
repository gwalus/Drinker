using DataDownloader.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataDownloader.Helpers
{
    public class DataProvider
    {
        private readonly HttpClient _httpClient = new HttpClient();


        /// <summary>Gets the drink by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
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


        /// <summary>Creates the ingredients.</summary>
        /// <param name="drink">The drink.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IList<IngredientToSeed> CreateIngredients(DrinkItem drink)
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

            IList<Models.IngredientToSeed> ingredients = new List<Models.IngredientToSeed>();

            for (int i = 0; i < ingradientNames.Count; i++)
            {
                var ingredient = new Models.IngredientToSeed
                {
                    Name = ingradientNames.ElementAtOrDefault(i),
                    Measure = ingradientMeasures.ElementAtOrDefault(i)?.TrimEnd()
                };

                ingredients.Add(ingredient);
            }

            return ingredients;
        }


        /// <summary>Saves to json file.</summary>
        /// <param name="coctails">The coctails.</param>
        public void SaveToJsonFile(IList<CoctailToSeed> coctails)
        {
            var currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            var filePath = Path.Combine(currentDirectory, "Coctails.json");

            var jsonFile = JsonSerializer.Serialize(coctails);

            File.WriteAllText(filePath, jsonFile);
        }
    }
}
