using System;

namespace Orcus.Core.IoC
{
    public interface ContainerRegistry
    {
        void Register(Type from, Type to);
        void Register(Type from, Type to, string name);
        void RegisterInstance(Type from, object to);
        void RegisterSingleton(Type from, Type to);
    }
}
