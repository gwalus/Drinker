using AutoFixture;
using AutoFixture.AutoMoq;
using DrinkerAPI.Data;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.Extensions.DependencyInjection;
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

        public CoctailRepositoryTests(DependencySetupFixture dependencySetupFixture)
        {
            _serviceProvide = dependencySetupFixture.ServiceProvider;
            _serviceScope = _serviceProvide.CreateScope();

            _context = _serviceScope.ServiceProvider.GetRequiredService<CoctailContext>();
            _coctailRepository = _serviceScope.ServiceProvider.GetRequiredService<ICoctailRepository>();

            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public async Task ShouldReturnCorrectIdFromDatabase()
        {
            //Arrange
            const int extectedIdResult = 1;
            var coctailToAdd = _fixture.Create<CoctailToAdd>();

            //Act
            var actualIdResult = await _coctailRepository.AddCoctail(coctailToAdd, 1);

            //Assert
            Assert.Equal(extectedIdResult, actualIdResult);
        }
    }
}
