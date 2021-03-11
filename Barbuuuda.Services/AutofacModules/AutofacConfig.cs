using Autofac;
using Barbuuuda.Core.Interfaces;

namespace Barbuuuda.Services.AutofacModules
{
    /// <summary>
    /// Конфигурация Autofac, в которой регистрируются все сервисы.
    /// </summary>
    public class AutofacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Сервис пользователя.
            builder.RegisterType<UserService>().As<IUser>();

            // Сервис стартовой страницы.
            builder.RegisterType<MainPageService>().As<IMainPage>();

            // Сервис заданий.
            builder.RegisterType<TaskService>().As<ITask>();

            // Сервис исполнителя.
            builder.RegisterType<ExecutorService>().As<IExecutor>();

            // Сервис пагинации.
            builder.RegisterType<PaginationService>().As<IPagination>();
        }
    }
}
