namespace Orcus.Core.IoC
{
    public interface ContainerAdapter<TContainer> : ContainerAdapter
    {
        TContainer Instance { get; }
    }
}
