using Autofac;
using AutoMapper;
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
        }
    }
}
