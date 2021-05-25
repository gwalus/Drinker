using DrinkerAPI.Dtos;
using DrinkerAPI.Extensions;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICoctailRepository _coctailRepostiory;

        public AdminController(ICoctailRepository coctailRepostiory)
        {
            _coctailRepostiory = coctailRepostiory;
        }

        [HttpGet(ApiRoutes.Admin.acceptCoctail)]
        public async Task<ActionResult> AcceptCoctail(int id)
        {
            var accepted = await _coctailRepostiory.AcceptCoctail(id);
            if (accepted == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet(ApiRoutes.Admin.rejectCoctail)]
        public async Task<ActionResult> RejectCoctail(int id)
        {
            var accepted = await _coctailRepostiory.RejectCoctail(id);
            if (accepted == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet(ApiRoutes.Admin.cocktailsToAccept)]
        public async Task<ActionResult<ICollection<CoctailDto>>> GetCocktailsToAccept([FromQuery] PaginationParams paginationParams)
        {
            var coctails = await _coctailRepostiory.GetCoctailsToAccept(paginationParams);

            if (coctails != null)
            {
                Response.AddPaginationHeader(coctails.CurrentPage, coctails.PageSize, coctails.TotalCount, coctails.TotalPages);

                return Ok(coctails);
            }

            return BadRequest("Unable to connect...");
        }
    }
}
