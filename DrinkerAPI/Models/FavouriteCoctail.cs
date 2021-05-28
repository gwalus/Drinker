using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DrinkerAPI.Models
{
    public class FavouriteCoctail
    {
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public int CoctailId { get; set; }
        public virtual Coctail Coctail { get; set; }
    }
}
