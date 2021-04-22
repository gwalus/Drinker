using DrinkerAPI.Helpers;
using DrinkerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface ICoctailRepository
    {
        /// <summary>Gets the list of coctails asynchronous.</summary>
        /// <param name="coctailParams">The coctail parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<PagedList<Coctail>> GetListOfCoctailsAsync(PaginationParams paginationParams);

        /// <summary>Gets the coctails by ingredients asynchronous.</summary>
        /// <param name="ingredients">The ingredients.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<PagedList<Coctail>> GetCoctailsByIngredientsAsync(IList<string> ingredients, CoctailParams coctailParams);

        /// <summary>Gets the coctail by name asynchronous.</summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<Coctail> GetCoctailByNameAsync(string keyword);
    }
}
