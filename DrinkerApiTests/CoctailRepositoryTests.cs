using AutoFixture;
using AutoFixture.AutoMoq;
using DrinkerAPI.Data;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace DrinkerApiTests
{
    public class CoctailRepositoryTests : IClassFixture<DependencySetupFixture>
    {
        private readonly IServiceScope _serviceScope;
        private readonly ServiceProvider _serviceProvide;
        private readonly CoctailContext _context;
        private readonly ICoctailRepository _coctailRepository;
        private readonly IFixture _fixture;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public CoctailRepositoryTests(DependencySetupFixture dependencySetupFixture)
        {
            _serviceProvide = dependencySetupFixture.ServiceProvider;
            _serviceScope = _serviceProvide.CreateScope();

            _context = _serviceScope.ServiceProvider.GetRequiredService<CoctailContext>();
            _coctailRepository = _serviceScope.ServiceProvider.GetRequiredService<ICoctailRepository>();

            _userManager = _serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            _roleManager = _serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            var coctailsData = Task.Run(() => File.ReadAllTextAsync(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "DrinkerApiTests/SeedForTesting.json"))).Result;

            var options = new JsonSerializerOptions
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };

            var coctails = JsonSerializer.Deserialize<List<Coctail>>(coctailsData, options);

            foreach (var item in coctails)
            {
                item.IsAccepted = true;
                Task.Run(() => _context.Coctails.AddAsync(item)).Wait();
            }

            Task.Run(() => _context.SaveChangesAsync()).Wait();

            var roles = new List<IdentityRole<int>>{
                new IdentityRole<int>{Name="User"},
                new IdentityRole<int>{Name="Admin"}
            };

            foreach (var role in roles)
            {
                Task.Run(() => _roleManager.CreateAsync(role)).Wait();
            }

            var admin = new AppUser { UserName = "admin", Email = "admin@gmail.com" };
            var defaultUser = new AppUser { UserName = "user", Email = "user@gmail.com" };

            Task.Run(() =>
            {
                _userManager.CreateAsync(admin, "Pa$$w0rd");
                _userManager.CreateAsync(defaultUser, "Pa$$w0rd");

                _userManager.AddToRoleAsync(admin, "Admin");
                _userManager.AddToRoleAsync(defaultUser, "User");
            }).Wait();            
        }

        [Fact]
        public async Task AddCoctail_ShouldReturnCorrectIdFromDatabase()
        {
            //Arrange
            const int extectedIdResult = 11023;
            var coctailToAdd = _fixture.Create<CoctailToAdd>();

            //Act
            var actualIdResult = await _coctailRepository.AddCoctail(coctailToAdd, 1);

            //Assert
            Assert.Equal(extectedIdResult, actualIdResult);
        }        
    }
}
