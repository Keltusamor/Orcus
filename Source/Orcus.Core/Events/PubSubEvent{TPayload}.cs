using System;
using System.Diagnostics;
using Orcus.Core.Events.Subscriptions;

namespace Orcus.Core.Events
{
    public class PubSubEvent<TPayload> : EventBase
    {
        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption = ThreadOption.PublisherThread, bool keepSubscriberReferenceAlive = false, Predicate<TPayload> filter = null)
        {
            var actionReference = new ManagedDelegateReference(action, keepSubscriberReferenceAlive);
            var filterReference = filter != null
                ? new ManagedDelegateReference(filter, keepSubscriberReferenceAlive)
                : new ManagedDelegateReference(new Predicate<TPayload>((param) => true), true);

            IEventSubscription eventSubscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    eventSubscription = new EventSubscription<TPayload>(actionReference, filterReference);
                    break;
                case ThreadOption.BackgroundThread:
                    eventSubscription = new BackgroundEventSubscription<TPayload>(actionReference, filterReference);
                    break;
                case ThreadOption.UIThread:
                    if (SynchronizationContext == null)
                        throw new InvalidOperationException("EventAggregator can't dispatch to UIThread because the eventAggregator was not created on the UIThread.");
                    eventSubscription = new DispatchedEventSubscription<TPayload>(actionReference, filterReference, SynchronizationContext);
                    break;
                default:
                    Debug.Assert(false, "Should never happen.");
                    throw new NotSupportedException($"Given {typeof(ThreadOption).FullName}: {threadOption} is not supported.");
            }

            return InternalSubscribe(eventSubscription);
        }

        public void Publish(TPayload payload)
        {
            InternalPublish(payload);
        }
    }
}
