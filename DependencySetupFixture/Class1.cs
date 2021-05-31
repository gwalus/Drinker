using Microsoft.Extensions.Configuration;
using System;

namespace DependencySetupFixture
{
    public class Class1
    {
        IConfigurationRoot _configurationRoot;

        public DependencySetupFixture()
        {
            _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            .AddJsonFile("testsSettings.json")
            .Build();

            var serviceCollection = new ServiceCollection();
            //serviceCollection.AddDbContext<CoctailContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            var connStr = _configurationRoot.GetConnectionString("DefaultConnection");

            serviceCollection.AddDbContext<CoctailContext>(options => options.UseNpgsql(connStr));


            serviceCollection.AddScoped<ICoctailRepository, CoctailRepository>();
            serviceCollection.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            serviceCollection.AddScoped<ICloudinaryService, CloudinaryService>();
            serviceCollection.Configure<CloudinarySettings>(_configurationRoot.GetSection("CloudinarySettings"));

            serviceCollection.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddRoles<IdentityRole<int>>()
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
              .AddEntityFrameworkStores<CoctailContext>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}
