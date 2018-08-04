using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Orcus.Core.Events.Subscriptions;

namespace Orcus.Core.Events
{
    public abstract class EventBase
    {
        public SynchronizationContext SynchronizationContext { get; set; }

        internal List<IEventSubscription> Subscriptions { get; } = new List<IEventSubscription>();

        internal SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            if (eventSubscription == null)
                throw new ArgumentNullException(nameof(eventSubscription));

            eventSubscription.SubscriptionToken = new SubscriptionToken(UnSubscribe);

            lock (Subscriptions)
            {
                Subscriptions.Add(eventSubscription);
            }

            return eventSubscription.SubscriptionToken;
        }

        public void UnSubscribe(SubscriptionToken subscriptionToken)
        {
            lock (Subscriptions)
            {
                var eventSubscription = Subscriptions.FirstOrDefault(subscription => subscription.SubscriptionToken == subscriptionToken);
                if (eventSubscription != null)
                    Subscriptions.Remove(eventSubscription);
            }
        }

        protected void InternalPublish(object argument)
        {
            foreach (var callback in PruneAndReturnSubscriptionCallbacks())
            {
                callback(argument);
            }
        }

        private List<Action<object>> PruneAndReturnSubscriptionCallbacks()
        {
            var nonGarbageCollectedSubscriptionCallbacks = new List<Action<object>>();

            lock (Subscriptions)
            {
                foreach (var subscription in Subscriptions.Reverse<IEventSubscription>())
                {
                    var callback = subscription.GetSubscriptionCallback();
                    if (callback == null)
                    {
                        Subscriptions.Remove(subscription);
                    }
                    else
                    {
                        nonGarbageCollectedSubscriptionCallbacks.Add(callback);
                    }
                }
            }

            return nonGarbageCollectedSubscriptionCallbacks;
        }
    }
}
