using AutoFixture;
using AutoFixture.AutoMoq;
using DrinkerAPI.Data;
using DrinkerAPI.Dtos;
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

        [Fact]
        public async Task GetCoctailDtoByIdAsync_ShouldReturnExpectedValues()
        {
            //Arrange
            const int id = 11000;
            var extectedIdResult = new CoctailDto
            {
                Id = 11000,
                Name = "Mojito",
                Category = "Cocktail",
                Alcoholic = "Alcoholic",
                Glass = "Highball glass",
                Instructions = "Muddle mint leaves with sugar and lime juice. Add a splash of soda water and fill the glass with cracked ice. Pour the rum and top with soda water. Garnish and serve with straw.",
                PhotoUrl = "https://www.thecocktaildb.com/images/media/drink/metwgh1606770327.jpg",
                DateModified = "2016-11-04 09:17:09",
                Ingradients = new List<IngredientDto>
                {
                    new IngredientDto
                    {
                        Name = "Light rum",
                        Measure = "2-3 oz"
                    },
                    new IngredientDto
                    {
                        Name = "Lime",
                        Measure = "Juice of 1"
                    },
                    new IngredientDto
                    {
                        Name = "Sugar",
                        Measure = "2 tsp"
                    },
                    new IngredientDto
                    {
                        Name = "Mint",
                        Measure = "2-4"
                    },
                    new IngredientDto
                    {
                        Name = "Soda water",
                        Measure = null
                    }
                }
            };

            //Act
            var actualIdResult = await _coctailRepository.GetCoctailDtoByIdAsync(id);

            //Assert
            Assert.Equal(extectedIdResult.Id, actualIdResult.Id);
            Assert.Equal(extectedIdResult.Name, actualIdResult.Name);
            Assert.Equal(extectedIdResult.Category, actualIdResult.Category);
            Assert.Equal(extectedIdResult.Alcoholic, actualIdResult.Alcoholic);
            Assert.Equal(extectedIdResult.Glass, actualIdResult.Glass);
            Assert.Equal(extectedIdResult.Instructions, actualIdResult.Instructions);
            Assert.Equal(extectedIdResult.PhotoUrl, actualIdResult.PhotoUrl);
            Assert.Equal(extectedIdResult.DateModified, actualIdResult.DateModified);

            for (int i = 0; i < extectedIdResult.Ingradients.Count; i++)
            {
                Assert.Equal(extectedIdResult.Ingradients[i].Name, actualIdResult.Ingradients[i].Name);
                Assert.Equal(extectedIdResult.Ingradients[i].Measure, actualIdResult.Ingradients[i].Measure);
            }
        }
    }
}
