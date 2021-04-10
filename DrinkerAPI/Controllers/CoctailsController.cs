using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CoctailsController : ControllerBase
    {
        [HttpGet("CheckDrinByName")]
        public string Get()
        {
            return "Test";
        }
    }
}
