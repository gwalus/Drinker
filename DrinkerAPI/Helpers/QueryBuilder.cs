using DrinkerAPI.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DrinkerAPI.Helpers
{
    public static class QueryBuilder
    {

        /// <summary>Builds the ingredients query.</summary>
        /// <param name="query">The query.</param>
        /// <param name="ingredients">The ingredients.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IQueryable<CoctailDto> BuildIngredientsQuery(IQueryable<CoctailDto> query, IList<string> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                query = query.Where(p => p.Ingradients.Any(k => k.Name == ingredient));
            }

            return query;
        }


        /// <summary>Adds the filters query.</summary>
        /// <param name="query">The query.</param>
        /// <param name="coctailParams">The coctail parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IQueryable<CoctailDto> AddFiltersQuery(IQueryable<CoctailDto> query, CoctailParams coctailParams)
        {
            if(!string.IsNullOrEmpty(coctailParams.Glass))
                query = query.Where(c => c.Glass == coctailParams.Glass);

            return query
                .Where(c => c.Alcoholic == coctailParams.Alcoholic)
                .Where(c => c.Category == coctailParams.Category);        
        }
    }
}
