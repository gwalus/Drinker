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
    }
}
