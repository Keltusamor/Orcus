using System;
using Orcus.Core.IoC;
using Unity;

namespace Orcus.GtkSharp3.Unity
{
    public class UnityContainerAdapter : IContainerAdapter<IUnityContainer>
    {
        public IUnityContainer Instance { get; }

        public bool SupportsModules => true;

        public UnityContainerAdapter()
            : this(new UnityContainer())
        {
        }

        public UnityContainerAdapter(IUnityContainer container)
        {
            Instance = container;
        }

        public object Resolve(Type type)
        {
            return Instance.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return Instance.Resolve(type, name);
        }

        public void Register(Type from, Type to)
        {
            Instance.RegisterType(from, to);
        }

        public void Register(Type from, Type to, string name)
        {
            Instance.RegisterType(from, to, name);
        }

        public void RegisterInstance(Type from, object to)
        {
            Instance.RegisterInstance(from, to);
        }

        public void RegisterSingleton(Type from, Type to)
        {
            Instance.RegisterSingleton(from, to);
        }

        public void FinishRegistration()
        {
        }
    }
}
