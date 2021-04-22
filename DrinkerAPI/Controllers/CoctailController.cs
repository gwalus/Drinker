﻿using DrinkerAPI.Extensions;
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

        /// <summary>
        /// Method answear Get request for List of every coctail that is in db
        /// </summary>
        /// <returns>list of coctails / errorMessage </returns>
        [HttpGet("list/all")]
        public async Task<ActionResult<ICollection<Coctail>>> GetCoctailsAsync([FromQuery] PaginationParams paginationParams)
        {
            var coctails = await _coctailRepository.GetListOfCoctailsAsync(paginationParams);
            
            if (coctails != null)
            {
                Response.AddPaginationHeader(coctails.CurrentPage, coctails.PageSize, coctails.TotalCount, coctails.TotalPages);
                
                return Ok(coctails);
            }

            return BadRequest("Unable to connect...");
        }


        /// <summary>Gets the coctails by ingredient.</summary>
        /// <param name="ingredients">The ingredients.</param>
        /// <param name="coctailParams">The coctail parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet("search/byIngredients")]
        public async Task<ActionResult<IList<Coctail>>> GetCoctailsByIngredient([FromQuery] IList<string> ingredients, [FromQuery] CoctailParams coctailParams)
        {
            if (ingredients.Count == 0) 
                return BadRequest("Please to add minimum one ingredient");

            var coctails = await _coctailRepository.GetCoctailsByIngredientsAsync(ingredients, coctailParams);

            if(coctails != null)
            {
                Response.AddPaginationHeader(coctails.CurrentPage, coctails.PageSize, coctails.TotalCount, coctails.TotalPages);
                
                return Ok(coctails);
            }

            return NotFound("No cocktails found with the specified filters.");
        }


        /// <summary>Gets the coctail by name.</summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet("search/byName/{keyword}")]
        public async Task<ActionResult<Coctail>> GetCoctailByName(string keyword)
        {
            var coctail = await _coctailRepository.GetCoctailByNameAsync(keyword);
            if (coctail != null)
                return Ok(coctail);

            return BadRequest("Coctail not found");
        }
    }
}
