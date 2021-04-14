using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<ICollection<Coctail>>> GetCoctailsAsync()
        {
            var coctails = await _coctailRepository.GetListOfCoctailsAsync();
            if (coctails != null)
                return Ok(coctails);

            return BadRequest("Unable to connect...");
        }
        [HttpGet("search/byIngredient/{keyword}")]
        public async Task<ActionResult<IList<Coctail>>> GetCoctailsByIngredient(string keyword)
        {
            var coctail = await _coctailRepository.GetCoctailsByIngredient(keyword);
            if (coctail != null)
                return Ok(coctail);

            return BadRequest("Coctail not found");

        }
        [HttpGet("search/byName/{keyword}")]
        public async Task<ActionResult<Coctail>> GetCoctailByName(string keyword)
        {
            var coctail = await _coctailRepository.GetCoctailByName(keyword);
            if (coctail != null)
                return Ok(coctail);

            return BadRequest("Coctail not found");
        }
    }
}
