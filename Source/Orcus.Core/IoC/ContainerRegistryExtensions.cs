using System;

namespace Orcus.Core.IoC
{
    public static class ContainerRegistryExtensions
    {
        public static void Register(this ContainerRegistry containerRegistry, Type type)
        {
            containerRegistry.Register(type, type);
        }

        public static void Register<T>(this ContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(T), typeof(T));
        }

        public static void Register(this ContainerRegistry containerRegistry, Type type, string name)
        {
            containerRegistry.Register(type, type, name);
        }

        public static void Register<T>(this ContainerRegistry containerRegistry, string name)
        {
            containerRegistry.Register(typeof(T), typeof(T), name);
        }

        public static void Register<TFrom, TTo>(this ContainerRegistry containerRegistry)
            where TTo : TFrom
        {
            containerRegistry.Register(typeof(TFrom), typeof(TTo));
        }

        public static void Register<TFrom, TTo>(this ContainerRegistry containerRegistry, string name)
            where TTo : TFrom
        {
            containerRegistry.Register(typeof(TFrom), typeof(TTo), name);
        }

        public static void RegisterInstance<TInterface>(this ContainerRegistry containerRegistry, TInterface instance)
        {
            containerRegistry.RegisterInstance(typeof(TInterface), instance);
        }

        public static void RegisterSingleton(this ContainerRegistry containerRegistry, Type type)
        {
            containerRegistry.RegisterSingleton(type, type);
        }

        public static void RegisterSingleton<T>(this ContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(T), typeof(T));
        }

        public static void RegisterSingleton<TFrom, TTo>(this ContainerRegistry containerRegistry)
            where TTo : TFrom
        {
            containerRegistry.RegisterSingleton(typeof(TFrom), typeof(TTo));
        }
    }
}
