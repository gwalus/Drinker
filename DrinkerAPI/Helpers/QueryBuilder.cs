using DrinkerAPI.Models;
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
        public static IQueryable<Coctail> BuildIngredientsQuery(IQueryable<Coctail> query, IList<string> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                query = query.Where(p => p.Ingradients.Any(k => k.Name == ingredient));
            }

            return query;
        }

        public static IQueryable<Coctail> AddFiltersQuery(IQueryable<Coctail> query, CoctailParams coctailParams)
        {
            query = query.Where(c => c.Alcoholic == coctailParams.Alcoholic);

            query = query.Where(c => c.Category == coctailParams.Category);

            query = query.Where(c => c.Glass == coctailParams.Glass);

            return query;
        }
    }
}
