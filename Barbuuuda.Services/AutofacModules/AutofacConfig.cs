using Autofac;
using AutoMapper;
using Barbuuuda.Core.Interfaces;
using System.Collections.Generic;

namespace Barbuuuda.Services.AutofacModules
{
    /// <summary>
    /// Конфигурация Autofac, в которой регистрируются все сервисы и AutoMapper.
    /// </summary>
    public class AutofacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Регистрирует IMapper.
            builder.RegisterAssemblyTypes(typeof(AutofacConfig).Assembly).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (Profile profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                    //cfg.CreateMap<UserEntity, UserOutput>();
                    //cfg.AddMaps(new[] { typeof(AutofacConfig).Assembly });
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

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
