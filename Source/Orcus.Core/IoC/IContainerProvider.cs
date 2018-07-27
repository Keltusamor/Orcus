using System;

namespace Orcus.Core.IoC
{
    public interface IContainerProvider
    {
        void Resolve(Type type);
        void Resolve(Type type, string name);
    }
}
