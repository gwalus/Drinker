using System.Collections.Generic;

namespace DrinkerAPI.Models
{
    public class Coctail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public string PhotoUrl { get; set; }
        public string DateModified { get; set; }
        public virtual IList<Ingredient> Ingradients { get; set; }
        public int? UserId { get; set; }
        public bool IsAccepted{get;set;}
        public virtual ICollection<FavouriteCoctail> FavouritedByUsers { get; set; }
    }
}
