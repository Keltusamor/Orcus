namespace Orcus.Core.Events
{
    public interface IEventAggregator
    {
        TEvent GetEvent<TEvent>()
            where TEvent : EventBase,
            new();
    }
}
