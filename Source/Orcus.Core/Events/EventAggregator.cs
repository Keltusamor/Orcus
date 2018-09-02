namespace Orcus.Core.Events
{
    public interface EventAggregator
    {
        TEvent GetEvent<TEvent>()
            where TEvent : EventBase
            , new();
    }
}
