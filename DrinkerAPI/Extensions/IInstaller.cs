using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DrinkerAPI.Extensions
{
    interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
