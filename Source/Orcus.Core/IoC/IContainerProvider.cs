using System;

namespace Orcus.Core.IoC
{
    public interface IContainerProvider
    {
        object Resolve(Type type);
        object Resolve(Type type, string name);
    }
}
