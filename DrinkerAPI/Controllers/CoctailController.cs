using DrinkerAPI.Dtos;
using DrinkerAPI.Extensions;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DrinkerAPI.Controllers
{
    public class CoctailController : BaseApiController
    {
        private readonly ICoctailRepository _coctailRepository;
        private readonly IUserRepository _userRepository;

        public CoctailController(ICoctailRepository coctailRepostiory, IUserRepository userRepository)
        {
            _coctailRepository = coctailRepostiory;
            _userRepository = userRepository;
        }

        /// <summary>Gets the coctails by ingredient.</summary>
        /// <param name="ingredients">The ingredients.</param>
        /// <param name="coctailParams">The coctail parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet(ApiRoutes.Coctails.All)]
        public async Task<ActionResult<IList<Coctail>>> GetCoctails(string name, [FromQuery] IList<string> ingredients, [FromQuery] CoctailParams coctailParams)
        {
            var coctails = await _coctailRepository.GetCoctailsAsync(name, ingredients, coctailParams);

            if (coctails != null)
            {
                Response.AddPaginationHeader(coctails.CurrentPage, coctails.PageSize, coctails.TotalCount, coctails.TotalPages);

                return Ok(coctails);
            }

            return NotFound("No cocktails found with the specified filters.");
        }


        /// <summary>Gets coctails contains keyword.</summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet(ApiRoutes.Coctails.ByName)]
        public async Task<ActionResult<IList<CoctailDto>>> GetCoctailsByName(string keyword)
        {
            var coctail = await _coctailRepository.GetCoctailsByNameAsync(keyword);
            if (coctail != null)
                return Ok(coctail);

            return BadRequest("Coctail not found");
        }

        [HttpGet(ApiRoutes.Coctails.Random)]
        public async Task<ActionResult<CoctailDto>> GetRandomCoctails(int count = 1)
        {
            var coctail = await _coctailRepository.GetRandomCoctailsAsync(count);
            if (coctail != null)
                return Ok(coctail);

            return NotFound("Coctail not found");
        }

        [HttpGet(ApiRoutes.Coctails.categories)]
        public async Task<ActionResult<List<string>>> GetCoctailCategories()
        {
            var categories = await _coctailRepository.GetCoctailCategories();

            if (categories != null)
                return Ok(categories);

            return BadRequest();
        }

        [HttpGet(ApiRoutes.Coctails.glasses)]
        public async Task<ActionResult<List<string>>> GetCoctailGlasses()
        {
            var glasses = await _coctailRepository.GetCoctailGlasses();

            if (glasses != null)
                return Ok(glasses);

            return BadRequest();
        }
        [HttpGet(ApiRoutes.Coctails.byId)]
        public async Task<ActionResult<CoctailDto>> GetCoctailById(int id)
        {
            var coctail = await _coctailRepository.GetCoctailDtoByIdAsync(id);

            if (coctail != null)
                return Ok(coctail);

            return BadRequest();
        }

        [HttpGet(ApiRoutes.Coctails.Names)]
        public async Task<ActionResult<CoctailDto>> GetCoctailNames()
        {
            var coctail = await _coctailRepository.GetCoctailNamesAsync();

            if (coctail != null)
                return Ok(coctail);

            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Coctails.addCoctailAsUser)]
        public async Task<ActionResult<int>> AddCoctail([FromBody] CoctailToAdd coctail)
        {
            var userId = User.GetUserId();

            int cocktailId = await _coctailRepository.AddCoctail(coctail, userId);

            if (cocktailId != 0)
            {
                return cocktailId;
            }

            return BadRequest("Something went wrong...");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Coctails.addPhotoToCocktail)]
        public async Task<ActionResult<bool>> AddCoctail([FromForm] PhotoToAdd photoToAdd)
        {
            if (photoToAdd.File.Length > 0)
            {
                return await _coctailRepository.AddPhotoToCocktail(photoToAdd.File, photoToAdd.CocktailId);
            }

            return BadRequest("Something went wrong...");
        }

        [HttpGet(ApiRoutes.Coctails.ingredientNames)]
        public async Task<ActionResult<IList<string>>> GetIngredientNames() => Ok(await _coctailRepository.GetIngredientNamesAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Coctails.addToFavourite)]
        public async Task<ActionResult> AddToFavourite(int cocktailId)
        {
            if (cocktailId == 0)
                return BadRequest();

            var userId = User.GetUserId();
            var cocktail = await _coctailRepository.GetCoctailByIdAsync(cocktailId);
            var user = await _userRepository.GetUserById(userId);

            if (user == null || cocktail == null)
                return NotFound("User or cocktail not found");


            var favouriteCocktail = new FavouriteCoctail
            {
                AppUser = user,
                AppUserId = userId,
                CoctailId = cocktailId,
                Coctail = cocktail
            };

            if (await _coctailRepository.AddCocktailToFavourite(favouriteCocktail))
                return Ok(JsonSerializer.Serialize("Cocktail has been added to favourite"));

            return BadRequest(JsonSerializer.Serialize("Something went wrong..."));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.Coctails.getFavourites)]
        public async Task<ActionResult<IList<CoctailDto>>> GetFavouritesCocktails([FromQuery] PaginationParams paginationParams)
        {
            var userId = User.GetUserId();

            var cocktails = await _coctailRepository.GetFavouritesCocktails(userId, paginationParams);

            if (userId == 0 || cocktails is null)
                return NotFound();

            Response.AddPaginationHeader(cocktails.CurrentPage, cocktails.PageSize, cocktails.TotalCount, cocktails.TotalPages);

            return Ok(cocktails);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(ApiRoutes.Coctails.deleteFromFavourites)]
        public async Task<ActionResult> DeleteFromFavouritesCocktails(int cocktailId)
        {
            var userId = User.GetUserId();
            if (userId == 0)
                return NotFound();

            var cocktailToDelete = await _coctailRepository.GetFavouriteCoctailAsync(userId, cocktailId);
            if (cocktailToDelete == null)
                return NotFound(JsonSerializer.Serialize("Cocktail not found"));


            var result = await _coctailRepository.DeleteFromFavouritesAsync(cocktailToDelete);

            if (result)
                return Ok(JsonSerializer.Serialize("Cocktail has been delete from favourited"));

            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.Coctails.isFavourite)]
        public async Task<ActionResult<bool>> IsCocktailFavourite(int cocktailId)
        {
            var userId = User.GetUserId();
            if (userId == 0)
                return NotFound();

            return await _coctailRepository.IsCocktailFavouriteAsync(userId, cocktailId);
        }
    }
}
