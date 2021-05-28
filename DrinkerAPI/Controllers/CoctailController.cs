using DrinkerAPI.Dtos;
using DrinkerAPI.Extensions;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkerAPI.Controllers
{
    public class CoctailController : BaseApiController
    {
        private readonly ICoctailRepository _coctailRepository;
        public CoctailController(ICoctailRepository coctailRepostiory)
        {
            _coctailRepository = coctailRepostiory;
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
            var coctail = await _coctailRepository.GetCoctailByIdAsync(id);

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

        [HttpPost(ApiRoutes.Coctails.addCoctailAsUser)]
        public async Task<ActionResult> AddCoctail([FromBody] Coctail coctail)
        {
            if (coctail != null)
            {
                var newCoctail = await _coctailRepository.AddCoctail(coctail);
                if (newCoctail == true)
                {
                    return Ok();
                }
            }
            return BadRequest("Something went wrong...");
        }

        [HttpGet(ApiRoutes.Coctails.ingredientNames)]
        public async Task<ActionResult<IList<string>>> GetIngredientNames() => Ok(await _coctailRepository.GetIngredientNamesAsync());
    }
}
