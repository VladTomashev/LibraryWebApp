using Microsoft.Extensions.Configuration;

namespace Library.Infrastructure.EntityFramework
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; }

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string GetConnectionString(string name)
        {
            return Configuration.GetConnectionString(name);
        }
    }
}
