using Barbuuuda.Core.Data;
using Barbuuuda.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Barbuuuda.Tests
{
    /// <summary>
    /// Базовый класс тестов с настройками конфигурации.
    /// </summary>
    public class BaseServiceTest
    {
        protected readonly string ACCOUNT = "lera";
        protected readonly string USER_ID = "b723e618-6e6a-41da-a1ac-50610fd4ae96";
        protected readonly long TASK_ID = 1000001;
        protected readonly string EXECUTOR_ID = "b723e618-6e6a-41da-a1ac-50610fd4ae96";
        protected readonly string EXECUTOR_LOGIN = "executor1";

        protected string MsSqlConfigString { get; set; }
        protected string PostgreConfigString { get; set; }
        protected IConfiguration AppConfiguration { get; set; }

        protected PostgreDbContext PostgreContext;
        protected UserService UserService;
        protected ExecutorService ExecutorService;

        public BaseServiceTest()
        {
            // Настройка тестовых строк подключения.
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            AppConfiguration = builder.Build();
            MsSqlConfigString = AppConfiguration["ConnectionStrings:TestMsSqlConnection"];
            PostgreConfigString = AppConfiguration["ConnectionStrings:TestNpgSqlConnection"];

            // Настройка тестовых контекстов.
            DbContextOptionsBuilder<PostgreDbContext> optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            optionsBuilder.UseNpgsql(PostgreConfigString);
            PostgreContext = new PostgreDbContext(optionsBuilder.Options);

            // Настройка экземпляров сервисов для тестов.
            UserService = new UserService(null, PostgreContext, null, null);
            ExecutorService = new ExecutorService(null, PostgreContext, UserService);
        }
    }
}
