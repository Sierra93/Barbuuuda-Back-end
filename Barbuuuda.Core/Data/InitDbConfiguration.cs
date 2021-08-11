using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barbuuuda.Core.Data
{
    public sealed class InitDbConfiguration
    {
        public static IConfiguration Configuration { get; set; }

        public InitDbConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static void Init(IServiceCollection services)
        {
            //test
            // TODO: ошибка если из свагера запустить наприер mainpage так как инициализируется лишь один раз при запуске контексты все эти. Если вернуть как было и то все работает.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TestMsSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));

            services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("TestNpgSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("TestNpgSqlConnection"), b => b.MigrationsAssembly("Barbuuuda.Core").EnableRetryOnFailure()));
        }
    }
}
