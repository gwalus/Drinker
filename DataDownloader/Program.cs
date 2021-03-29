using DataDownloader.Interfaces;
using DataDownloader.Services;
using System;
using System.Threading.Tasks;

namespace DataDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {            
            IDataService _dataService = new DataService();

            var onedrink = await _dataService.GetDrinkByIdAsync(11001);

            var ingredients = _dataService.CreateIngredients(onedrink);

            Console.ReadKey();
        }
    }
}
