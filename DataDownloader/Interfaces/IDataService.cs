using DataDownloader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDownloader.Interfaces
{
    /// <summary>
    /// Interface contains methods to operations on data.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Getting one element from coctailsDB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DrinkItem</returns>
        Task<DrinkItem> GetDrinkByIdAsync(int id);
        /// <summary>
        /// Create a list of igredients from reponse API properties.
        /// </summary>
        /// <param name="drink"></param>
        /// <returns>List of ingredients.</returns>
        IList<Ingredient> CreateIngredients(DrinkItem drink);
        /// <summary>
        /// Save drink to the database.
        /// </summary>
        /// <returns></returns>
        Task SaveDrinkAsync(DrinkBsonItem item);
    }
}
