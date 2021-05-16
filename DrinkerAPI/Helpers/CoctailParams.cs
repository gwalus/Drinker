using System.Collections.Generic;

namespace DrinkerAPI.Helpers
{
    public class CoctailParams : PaginationParams
    {
        public ICollection<string> Categories { get; set; }
        public ICollection<string> AlcoholicTypes { get; set; }
        public ICollection<string> Glasses { get; set; }
    }
}
