using NUnit.Framework;
using Orcus.Core.Tests.Mocks.Events;
using Orcus.Core.Tests.Mocks.Events.Subscriptions;

namespace Orcus.Core.Tests.Events
{
    [TestFixture]
    public class EventBaseFixture
    {
        [TestCase]
        public void CanSubscribeEvents()
        {
            var @event = new TestableMockEventBase();
            var subscription = new MockEventSubscription((arg) => { });

            var subscriptionToken = @event.Subscribe(subscription);

            Assert.NotNull(subscription.SubscriptionToken);
            Assert.AreSame(subscription.SubscriptionToken, subscriptionToken);
            Assert.IsTrue(@event.Contains(subscriptionToken));
        }

        [TestCase]
        public void CanUnSubscribeEvents()
        {
            var @event = new TestableMockEventBase();
            var eventPublished = false;
            var subscription = new MockEventSubscription((arg) => { eventPublished = true; });

            var subscriptionToken = @event.Subscribe(subscription);
            @event.UnSubscribe(subscriptionToken);
            @event.Publish(null);

            Assert.IsFalse(eventPublished);
            Assert.IsFalse(@event.Contains(subscriptionToken));
        }

        [TestCase]
        public void CanPublishEventsToSingleSubscriber()
        {
            var @event = new TestableMockEventBase();
            var eventPublished = false;
            var subscription = new MockEventSubscription((arg) => { eventPublished = true; });

            @event.Subscribe(subscription);
            @event.Publish(null);

            Assert.IsTrue(eventPublished);
        }

        [TestCase]
        public void CanPublishEventsWithPayloadToSingleSubscriber()
        {
            var @event = new TestableMockEventBase();
            var payload = new MockPayload();
            object received = null;
            var subscription = new MockEventSubscription((arg) => { received = arg; });

            @event.Subscribe(subscription);
            @event.Publish(payload);

            Assert.AreSame(payload, received);
        }

        [TestCase]
        public void CanPublishEventsToMultipleSubscribers()
        {
            var @event = new TestableMockEventBase();
            var event1Published = false;
            var subscription1 = new MockEventSubscription((arg) => { event1Published = true; });
            var event2Published = false;
            var subscription2 = new MockEventSubscription((arg) => { event2Published = true; });

            @event.Subscribe(subscription1);
            @event.Subscribe(subscription2);
            @event.Publish(null);

            Assert.IsTrue(event1Published);
            Assert.IsTrue(event2Published);
        }

        [TestCase]
        public void CanPublishEventsWithPayloadToMultipleSubscribers()
        {
            var @event = new TestableMockEventBase();
            var payload = new MockPayload();
            object received1 = null;
            var subscription1 = new MockEventSubscription((arg) => { received1 = arg; });
            object received2 = null;
            var subscription2 = new MockEventSubscription((arg) => { received2 = arg; });

            @event.Subscribe(subscription1);
            @event.Subscribe(subscription2);
            @event.Publish(payload);

            Assert.AreSame(payload, received1);
            Assert.AreSame(payload, received2);
        }

        [TestCase]
        public void SubscriptionIsPrunedIfActionIsNull()
        {
            var @event = new TestableMockEventBase();
            var subscription = new MockEventSubscription(null);
            var subscriptionToken = @event.Subscribe(subscription);

            @event.Publish(null);

            Assert.IsFalse(@event.Contains(subscriptionToken));
        }
    }
}
