using DrinkerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
