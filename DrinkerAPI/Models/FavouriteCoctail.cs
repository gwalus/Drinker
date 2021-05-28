using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DrinkerAPI.Models
{
    public class FavouriteCoctail
    {
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public int CoctailId { get; set; }
        public virtual Coctail Coctail { get; set; }
        public virtual ICollection<AppUser> FavouritedByUsers { get; set; }
    }
}
