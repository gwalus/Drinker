using DrinkerAPI.Contracts.Requests;
using DrinkerAPI.Contracts.Responses;
using DrinkerAPI.Data;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DrinkerAPI.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly CoctailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IdentityController(IIdentityService identityService, CoctailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _identityService = identityService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<ActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            await _userManager.AddToRoleAsync(user, "User");

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
    }
}
