using DrinkerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface ICoctailRepository
    {
        Task<ICollection<Coctail>> GetListOfCoctailsAsync();

        /// <summary>Gets the coctails by ingredients asynchronous.</summary>
        /// <param name="ingredients">The ingredients.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IList<Coctail>> GetCoctailsByIngredientsAsync(IList<string> ingredients);
        Task<Coctail> GetCoctailByName(string keyword);
    }
}
