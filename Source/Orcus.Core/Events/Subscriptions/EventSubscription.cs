using System;

namespace Orcus.Core.Events.Subscriptions
{
    interface EventSubscription
    {
        SubscriptionToken SubscriptionToken { get; set; }

        Action<object> GetSubscriptionCallback();
    }
}
