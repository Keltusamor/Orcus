namespace Orcus.Core.IoC
{
    public static class ContainerProviderExtensions
    {
        public static T Resolve<T>(this ContainerProvider containerProvider)
        {
            return (T)containerProvider.Resolve(typeof(T));
        }

        public static T Resolve<T>(this ContainerProvider containerProvider, string name)
        {
            return (T)containerProvider.Resolve(typeof(T), name);
        }
    }
}
