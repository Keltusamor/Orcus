using System;
using Orcus.Core.Events.Subscriptions;

namespace Orcus.Core.Tests.Mocks.Events.Subscriptions
{
    internal class MockEventSubscription : EventSubscription
    {
        private Action<object> Action { get; }

        public SubscriptionToken SubscriptionToken { get; set; }

        public bool GetSubscriptionCallbackCalled { get; private set; }

        public MockEventSubscription(Action<object> action)
        {
            Action = action;
        }

        public Action<object> GetSubscriptionCallback()
        {
            GetSubscriptionCallbackCalled = true;
            return Action;
        }
    }
}
