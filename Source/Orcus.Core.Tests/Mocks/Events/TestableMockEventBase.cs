using Orcus.Core.Events;
using Orcus.Core.Events.Subscriptions;

namespace Orcus.Core.Tests.Mocks.Events
{
    public class TestableMockEventBase : EventBase
    {
        internal SubscriptionToken Subscribe(EventSubscription eventSubscription)
        {
            return InternalSubscribe(eventSubscription);
        }

        public void Publish(object argument)
        {
            InternalPublish(argument);
        }
    }
}
