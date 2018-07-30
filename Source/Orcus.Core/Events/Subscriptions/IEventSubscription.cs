using System;

namespace Orcus.Core.Events.Subscriptions
{
    public interface IEventSubscription
    {
        SubscriptionToken SubscriptionToken { get; set; }

        Action<object> GetSubscriptionCallback();
    }
}
