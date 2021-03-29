using DataDownloader.Interfaces;
using DataDownloader.Models;
using DataDownloader.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const int ID = 11000;
            const int MAXID = 17841;

            int added = 0;

            IDataService _dataService = new DataService();
            IList<BsonDocument> bsonDrinkItems = new List<BsonDocument>();

            Console.WriteLine("Downloading data...");

            for (int i = ID; i < MAXID; i++)
            {
                var onedrink = await _dataService.GetDrinkByIdAsync(i);

                if (onedrink == null) continue;

                var ingredients = _dataService.CreateIngredients(onedrink);

                bsonDrinkItems = new List<BsonDocument>();
                
                foreach (var item in ingredients)
                {                    
                    var bsonItem = item.ToBsonDocument();
                    bsonDrinkItems.Add(bsonItem);
                }

                var drinkBsonItem = new DrinkBsonItem
                {
                    Alcoholic = onedrink.strAlcoholic,
                    Category = onedrink.strCategory,
                    DateModified = onedrink.dateModified,
                    DrinkId = onedrink.idDrink,
                    Glass = onedrink.strGlass,
                    Instructions = onedrink.strInstructions,
                    Name = onedrink.strDrink,
                    PhotoUrl = onedrink.strDrinkThumb,
                    Ingradients = bsonDrinkItems
                };

                await _dataService.SaveDrinkAsync(drinkBsonItem);
                added++;
            }

            Console.WriteLine($"{added} documents were downloaded and saved!");
            Console.ReadKey();
        }
    }
}
