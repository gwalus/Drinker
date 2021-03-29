using DataDownloader.Models;
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
        /// Save drink to the database.
        /// </summary>
        /// <returns></returns>
        Task SaveDrink(DrinkBsonItem item);
    }
}
