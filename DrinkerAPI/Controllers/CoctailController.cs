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
        [HttpGet("listAll")]
        public async Task<ActionResult<ICollection<Coctail>>> GetCoctailsAsync()
        {
            var coctails = await _coctailRepository.GetListOfCoctailsAsync();
            if (coctails != null)
                return Ok(coctails);

            return BadRequest("Unable to connect...");
        }
        
    }
}
