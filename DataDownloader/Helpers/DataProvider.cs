using DataDownloader.Models;
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
    }
}
