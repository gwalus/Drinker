using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Controllers
{
    public class CoctailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
