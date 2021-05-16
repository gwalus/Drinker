using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
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
    }
}
