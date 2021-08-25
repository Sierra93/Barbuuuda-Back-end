using Autofac;
using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;

namespace Barbuuuda.Services.AutofacModules
{
    //[CommonModule]
    public sealed class ServicesModule
    {
        public static void InitModules(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<PostgreDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().Named<IUserService>("UserService");

            builder.RegisterType<TaskService>().Named<ITaskService>("TaskService");

            // Сервис стартовой страницы.
            builder.RegisterType<MainPageService>().As<IMainPageService>();

            // Сервис пользователя.
            builder.RegisterType<UserService>().As<IUserService>();

            // Сервис стартовой страницы.
            builder.RegisterType<MainPageService>().As<IMainPageService>();

            // Сервис заданий.
            builder.RegisterType<TaskService>().As<ITaskService>();

            // Сервис исполнителя.
            builder.RegisterType<ExecutorService>().As<IExecutorService>();

            // Сервис пагинации.
            builder.RegisterType<PaginationService>().As<IPaginationService>();

            // Сервис БЗ.
            builder.RegisterType<KnowlegeService>().As<IKnowlegeService>();

            // Чат.
            builder.RegisterType<ChatService>().As<IChatService>();

            // Платежная система.
            builder.RegisterType<PaymentService>().As<IPaymentService>();
        }
    }
}
