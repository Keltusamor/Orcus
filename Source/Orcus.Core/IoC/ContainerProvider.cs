using System;

namespace Orcus.Core.IoC
{
    public interface ContainerProvider
    {
        object Resolve(Type type);
        object Resolve(Type type, string name);
    }
}
