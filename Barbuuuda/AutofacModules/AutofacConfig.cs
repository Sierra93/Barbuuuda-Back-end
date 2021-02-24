using Autofac;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Services;

namespace Barbuuuda.AutofacModules
{
    /// <summary>
    /// Конфигурация Autofac, в которой ругистрируются все сервисы.
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

            // TODO: когда будет проведен рефакторинг контроллера, вернуться сюда.
            // Сервис пагинации.
            //builder.RegisterType<ExecutorService>().As<IPagination>();
        }
    }
}
