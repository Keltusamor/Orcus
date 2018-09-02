using System;
using System.Collections.Generic;
using System.Threading;

namespace Orcus.Core.Events
{
    public class EventAggregatorImpl : EventAggregator
    {
        private Dictionary<Type, EventBase> Events { get; }

        private SynchronizationContext SynchronizationContext { get; }

        public EventAggregatorImpl()
        {
            Events = new Dictionary<Type, EventBase>();
            SynchronizationContext = SynchronizationContext.Current;
        }

        public TEvent GetEvent<TEvent>() where TEvent : EventBase, new()
        {
            lock (Events)
            {
                if (TryGetExistingEvent<TEvent>(out var existingEvent))
                    return existingEvent;

                return CreateNewEvent<TEvent>();
            }
        }

        private bool TryGetExistingEvent<TEvent>(out TEvent existingEvent) where TEvent : EventBase, new()
        {
            existingEvent = null;
            if (Events.TryGetValue(typeof(TEvent), out var @event))
                existingEvent = (TEvent)@event;
            return existingEvent != null;
        }

        private TEvent CreateNewEvent<TEvent>() where TEvent : EventBase, new()
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
