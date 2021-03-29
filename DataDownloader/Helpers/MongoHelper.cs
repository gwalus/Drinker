using Microsoft.Extensions.Configuration;

namespace DataDownloader.Helpers
{
    public class MongoHelper
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        public MongoHelper()
        {
            _configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .AddUserSecrets<Program>()
               .Build();

            _configuration.GetSection("MongoDb").Bind(this);
        }
    }
}
