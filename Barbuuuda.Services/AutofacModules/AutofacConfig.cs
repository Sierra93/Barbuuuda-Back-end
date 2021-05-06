using Autofac;
using AutoMapper;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.User;
using Barbuuuda.Models.User.Outpoot;
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
                    //cfg.CreateMap<UserEntity, UserOutpoot>();
                    //cfg.AddMaps(new[] { typeof(AutofacConfig).Assembly });
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

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

            // Сервис БЗ.
            builder.RegisterType<KnowlegeService>().As<IKnowlege>();

            // Чат.
            builder.RegisterType<ChatService>().As<IChat>();
        }
    }
}
