using Microsoft.AspNetCore.Http;

namespace DrinkerAPI.Dtos
{
    public class PhotoToAdd
    {
        public IFormFile File{ get; set; }
        public int CocktailId { get; set; }
    }
}
