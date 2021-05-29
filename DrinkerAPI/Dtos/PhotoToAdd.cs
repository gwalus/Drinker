using Microsoft.AspNetCore.Http;

namespace DrinkerAPI.Dtos
{
    public class PhotoToAdd
    {
        public IFormFile Photo{ get; set; }
        public int CocktailId { get; set; }
    }
}
