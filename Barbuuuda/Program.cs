using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Barbuuuda
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHostBuilder CreateHostBuilder(string[] args)
        //{
        //    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        //    var conf = builder.Build();

        //    IWebHostBuilder defaultBuilder = WebHost.CreateDefaultBuilder(args).ConfigureLogging(logging =>
        //    {
        //        logging.ClearProviders();
        //    });

        //    defaultBuilder.UseConfiguration(conf);

        //    return defaultBuilder.UseStartup<Startup>();
        //}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://*:9993")
                .UseStartup<Startup>();
    }
}
