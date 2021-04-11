using System.Collections.Generic;

namespace DataDownloader.Models
{
    public class CoctailToSeed
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public string PhotoUrl { get; set; }
        public string DateModified { get; set; }
        public ICollection<IngredientToSeed> Ingradients { get; set; }
    }
}
