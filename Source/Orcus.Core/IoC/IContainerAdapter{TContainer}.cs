namespace Orcus.Core.IoC
{
    public interface IContainerAdapter<TContainer> : IContainerAdapter
    {
        TContainer Instance { get; }
    }
}
