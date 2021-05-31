using AutoFixture;
using AutoFixture.AutoMoq;
using DrinkerAPI.Data;
using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DrinkerAPI.Tests.Integration
{
    public class CoctailRepositoryIntegrationTests : IClassFixture<DependencySetupFixtureForPostgres>
    {
        private readonly IServiceScope _serviceScope;
        private readonly ServiceProvider _serviceProvide;
        private readonly CoctailContext _context;
        private readonly ICoctailRepository _coctailRepository;
        private readonly IFixture _fixture;

        public CoctailRepositoryIntegrationTests(DependencySetupFixtureForPostgres dependencySetupFixture)
        {
            _serviceProvide = dependencySetupFixture.ServiceProvider;
            _serviceScope = _serviceProvide.CreateScope();
            _context = _serviceScope.ServiceProvider.GetRequiredService<CoctailContext>();

            _coctailRepository = _serviceScope.ServiceProvider.GetRequiredService<ICoctailRepository>();
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public async Task ShouldAddNewCoctailAndUploadPhotoToCloudinary()
        {
            //Arrange
            const int notAdded = 0;
            const int userId = 1;
            var coctailToAdd = _fixture.Create<CoctailToAdd>();

            //Act
            const string photoName = "spider.png";
            string path = Path.Combine(Directory.GetCurrentDirectory(), photoName);

            byte[] destinationData;

            using (FileStream fs = new FileStream(photoName, FileMode.Open, FileAccess.Read))
            {
                destinationData = File.ReadAllBytes(path);
                fs.Read(destinationData, 0, System.Convert.ToInt32(fs.Length));
            }

            int idFromDatabase = await _coctailRepository.AddCoctail(coctailToAdd, userId);
            bool isUploaded = await _coctailRepository.AddPhotoToCocktail(destinationData, photoName, idFromDatabase);

            //Act
            Assert.NotEqual(notAdded, idFromDatabase);
            Assert.True(isUploaded);
        }
    }
}
