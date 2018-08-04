using System;

namespace Orcus.Core.Events.Subscriptions
{
    interface IEventSubscription
    {
        SubscriptionToken SubscriptionToken { get; set; }

        Action<object> GetSubscriptionCallback();
    }
}
