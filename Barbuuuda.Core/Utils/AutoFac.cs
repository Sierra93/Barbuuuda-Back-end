﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Barbuuuda.Core.Attributes;
using Module = Autofac.Module;

namespace Barbuuuda.Core.Utils
{
    public static class AutoFac 
    {
        private static ContainerBuilder _builder;
        private static IContainer _container;
        private static IEnumerable<Type> _typeModules;

        /// <summary>
        /// Инициализация контейнера
        /// </summary>
        /// <param name="containerBuilderHandler">Делегат для дополнительной инициализации контейнера</param>
        public static IContainer Init(Action<ContainerBuilder> containerBuilderHandler = null)
        {
            if (_builder != null)
            {
                return _container;
            }

            _builder = new ContainerBuilder();

            var assemblies1 =
                GetAssembliesFromApplicationBaseDirectory(x => x.FullName.StartsWith("Barbuuuda.Services"));

            _builder.RegisterAssemblyTypes(assemblies1).AsImplementedInterfaces();

            var assemblies = assemblies1.Union(assemblies1);

            _typeModules = (from assembly in assemblies
                from type in assembly.GetTypes()
                where type.IsClass && type.GetCustomAttribute<CommonModuleAttribute>() != null
                select type).ToArray();

            containerBuilderHandler?.Invoke(_builder);

            foreach (var module in _typeModules)
            {
                _builder.RegisterModule(Activator.CreateInstance(module) as Module);
            }

            _container = _builder.Build();

            return _container;
        }

        private static Assembly[] GetAssembliesFromApplicationBaseDirectory(Func<AssemblyName, bool> condition)
        {
            var baseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            Func<string, bool> isAssembly = file =>
                string.Equals(Path.GetExtension(file), ".dll", StringComparison.OrdinalIgnoreCase);

            return Directory.GetFiles(baseDirectoryPath)
                .Where(isAssembly)
                .Where(f => condition(AssemblyName.GetAssemblyName(f)))
                .Select(Assembly.LoadFrom)
                .ToArray();
        }

        /// <summary>
        /// Получить сервис
        /// </summary>
        /// <typeparam name="TService">Тип сервиса</typeparam>
        /// <param name="notException">Не выдавать исключение если не удалось получить объект По умолчанию false</param> 
        /// <returns>Экземпляр запрашиваемого сервиса</returns>
        public static TService Resolve<TService>() where TService : class
        {
            if (_container == null)
            {
                _builder = new ContainerBuilder();
                _container = _builder.Build();
            }

            if (!_container.IsRegistered<TService>())
            {
                return null;
            }

            var service = _container.Resolve<TService>();

            return service;
        }

        public static ILifetimeScope CreateLifetimeScope()
        {
            if (_container == null)
            {
                _builder = new ContainerBuilder();
                _container = _builder.Build();
            }

            return _container.BeginLifetimeScope();
        }

        /// <summary>
        /// Получить сервис по уникальному имени
        /// </summary>
        /// <typeparam name="TService">Экземпляр запрашиваемого сервиса</typeparam>
        /// <param name="serviceName">Уникальное имя запрашиваемого типа</param>
        /// <param name="notException">Не выдавать исключение если не удалось получить объект По умолчанию false</param>
        /// <returns></returns>
        public static TService ResolveNamedScoped<TService>(this ILifetimeScope scope, string serviceName)
            where TService : class
        {
            if (!_container.IsRegisteredWithName<TService>(serviceName))
            {
                return null;
            }

            var service = _container.ResolveNamed<TService>(serviceName);

            return service;
        }
    }
}