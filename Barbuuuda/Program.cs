using Autofac;
using Autofac.Extensions.DependencyInjection;
using Barbuuuda.Services.AutofacModules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Barbuuuda
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AutofacConfig());
        })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
