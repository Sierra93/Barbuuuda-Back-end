using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Barbuuuda
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var conf = builder.Build();

            IWebHostBuilder defaultBuilder = WebHost.CreateDefaultBuilder(args).ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });
            
            defaultBuilder.UseConfiguration(conf);

            return defaultBuilder.UseStartup<Startup>();
        }
    }
}
