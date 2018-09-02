using System;
using System.Diagnostics;
using Orcus.Core.Events.Subscriptions;

namespace Orcus.Core.Events
{
    public class PubSubEvent : EventBase
    {
        public SubscriptionToken Subscribe(
            Action action
            , ThreadOption threadOption = ThreadOption.PublisherThread
            , bool keepSubscriberReferenceAlive = false
        )
        {
            var actionReference = new ManagedDelegateReference(action, keepSubscriberReferenceAlive);

            EventSubscription eventSubscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    eventSubscription = new PublisherEventSubscription(actionReference);
                    break;
                case ThreadOption.BackgroundThread:
                    eventSubscription = new BackgroundEventSubscription(actionReference);
                    break;
                case ThreadOption.UIThread:
                    if (SynchronizationContext == null)
                        throw new InvalidOperationException("EventAggregator can't dispatch to UIThread because the eventAggregator was not created on the UIThread.");
                    eventSubscription = new DispatchedEventSubscription(actionReference, SynchronizationContext);
                    break;
                default:
                    Debug.Assert(false, "Should never happen.");
                    throw new NotSupportedException($"Given {typeof(ThreadOption).FullName}: {threadOption} is not supported.");
            }

            return InternalSubscribe(eventSubscription);
        }

        public void Publish()
        {
            InternalPublish(null);
        }
    }
}
