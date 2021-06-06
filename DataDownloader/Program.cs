using System.Threading.Tasks;

namespace DataDownloader
{
    class Program
    {

        /// <summary>Defines the entry point of the application.</summary>
        static async Task Main()
        {
            //var dataProvider = new DataProvider();

            //const int ID = 11000;
            //const int MAXID = 17841;
            //int count = 0;

            //IList<CoctailToSeed> coctails = new List<CoctailToSeed>();

            //Console.WriteLine("Downloading data...");

            //for (int i = ID; i < MAXID; i++)
            //{
            //    var drink = await dataProvider.GetDrinkByIdAsync(i);
            //    if (drink == null) continue;

            //    var coctail = new CoctailToSeed
            //    {
            //        Alcoholic = drink.strAlcoholic,
            //        Category = drink.strCategory,
            //        DateModified = drink.dateModified,
            //        Id = drink.idDrink,
            //        Glass = drink.strGlass,
            //        Instructions = drink.strInstructions,
            //        Name = drink.strDrink,
            //        PhotoUrl = drink.strDrinkThumb
            //    };

            //    var ingredients = dataProvider.CreateIngredients(drink);
            //    coctail.Ingradients = ingredients;
            //    coctails.Add(coctail);
            //    count++;
            //}

            //Console.WriteLine($"Saved {count} json records");
            //dataProvider.SaveToJsonFile(coctails);
        }
    }
}
