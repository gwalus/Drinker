using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DrinkerAPI.Models
{
    public class AppUser : IdentityUser<int>
    {
        public virtual ICollection<FavouriteCoctail> FavouriteCoctails { get; set; }
    }
}
