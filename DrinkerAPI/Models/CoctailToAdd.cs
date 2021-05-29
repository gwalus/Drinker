using DrinkerAPI.Dtos;
using System.Collections.Generic;

namespace DrinkerAPI.Models
{
    public class CoctailToAdd
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public IList<IngredientDto> Ingradients { get; set; }
    }
}
