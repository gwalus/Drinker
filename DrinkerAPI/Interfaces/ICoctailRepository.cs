﻿using DrinkerAPI.Dtos;
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
        Task<PagedList<CoctailDto>> GetListOfCoctailsAsync(PaginationParams paginationParams);

        /// <summary>Gets the coctails by ingredients asynchronous.</summary>
        /// <param name="ingredients">The ingredients.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<PagedList<CoctailDto>> GetCoctailsByIngredientsAsync(IList<string> ingredients, CoctailParams coctailParams);

        /// <summary>Gets coctails contains keyword asynchronous.</summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IList<CoctailDto>> GetCoctailsByNameAsync(string keyword);

        /// <summary>Gets the random coctails asynchronous.</summary>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<List<CoctailDto>> GetRandomCoctailsAsync(int count);

        /// <summary>Gets the coctail categories.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<List<string>> GetCoctailCategories();

        /// <summary>Gets the coctail glasses.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<List<string>> GetCoctailGlasses();
        /// <summary>Get coctail by Id  </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<CoctailDto> GetCoctailByIdAsync(int Id);


        /// <summary>Gets the coctails name asynchronous.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IList<string>> GetCoctailNamesAsync();
        Task<bool> AddCoctail(Coctail coctail);
        Task<bool> AcceptCoctail(int Id);
        Task<bool> RejectCoctail(int Id);
    }
}
