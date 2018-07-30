using System;
using System.Collections.Generic;
using System.Threading;

namespace Orcus.Core.Events
{
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, EventBase> Events { get; }

        private SynchronizationContext SynchronizationContext { get; }

        public EventAggregator()
        {
            Events = new Dictionary<Type, EventBase>();
            SynchronizationContext = SynchronizationContext.Current;
        }

        public TEvent GetEvent<TEvent>() where TEvent : EventBase, new()
        {
            lock (Events)
            {
                if (Events.TryGetValue(typeof(TEvent), out var existingEvent))
                {
                    return (TEvent)existingEvent;
                }
                else
                {
                    var newEvent = new TEvent
                    {
                        SynchronizationContext = SynchronizationContext
                    };
                    Events[typeof(TEvent)] = newEvent;
                    return newEvent;
                }
            }
        }
    }
}
