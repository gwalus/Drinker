using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using DrinkerAPI.Options;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }
        public Task<AuthentiactionResult> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AuthentiactionResult> RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
