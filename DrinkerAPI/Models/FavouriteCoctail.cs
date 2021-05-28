using Microsoft.AspNetCore.Identity;

namespace DrinkerAPI.Models
{
    public class FavouriteCoctail
    {
        public int UserId { get; set; }
        public IdentityUser User { get; set; }
        public int CoctailId { get; set; }
        public Coctail Coctail { get; set; }
    }
}
