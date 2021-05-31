using Autofac;
using Barbuuuda.Commerces.Core;
using Barbuuuda.Commerces.Service;
using Barbuuuda.Core.Interfaces;

namespace Barbuuuda.Services.AutofacModules
{
    public partial class ServicesModule 
    {
        public static void InitModules(ContainerBuilder builder)
        {
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

            // Сервис PayPal.
            builder.RegisterType<PayPalService>().As<IPayPalService>();
        }
    }
}
