using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using DrinkerAPI.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DrinkerAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public IdentityService(UserManager<AppUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }
        public async Task<AuthentiactionResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthentiactionResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthentiactionResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            return await GenerateAuthenticationResultForUser(user);
        }

        public async Task<AuthentiactionResult> RegisterAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new AuthentiactionResult
                {
                    Errors = new[] { "User with this email address already exists" }
                };
            }
            var newUser = new AppUser
            {                
                Email = email,
                UserName = email,

            };
            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthentiactionResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            return await GenerateAuthenticationResultForUser(newUser);
        }
        private async Task<AuthentiactionResult> GenerateAuthenticationResultForUser(AppUser newUser)
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                new Claim(JwtRegisteredClaimNames.NameId, newUser.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(newUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            tokenDescriptor.Subject = new ClaimsIdentity(claims);

            var token = tokenHanlder.CreateToken(tokenDescriptor);
            return new AuthentiactionResult
            {
                Success = true,
                Token = tokenHanlder.WriteToken(token)
            };
        }
    }
}
