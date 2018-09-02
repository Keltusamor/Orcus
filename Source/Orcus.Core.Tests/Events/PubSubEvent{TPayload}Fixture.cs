using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Orcus.Core.Tests.Mocks.Events;

namespace Orcus.Core.Tests.Events
{
    [TestFixture]
    public class PubSubEvent_TPayload_Fixture
    {
        private TestableMockPubSubEvent<MockPayload> Event { get; set; }
        private MockPayload Payload { get; set; }

        [SetUp]
        public void SetUp()
        {
            Event = new TestableMockPubSubEvent<MockPayload>();
            Payload = new MockPayload();
        }

        [TestCase]
        public void CanSubscribeAndPublishGeneric()
        {
            var eventPublished = false;
            object received = null;

            Event.Subscribe((arg) => { eventPublished = true; received = arg; });
            Event.Publish(Payload);

            Assert.IsTrue(eventPublished);
            Assert.AreSame(Payload, received);
        }

        [TestCase]
        public void CanSubscribeAndPublishGenericToMultipleSubscribers()
        {
            var event1Published = false;
            object received1 = null;
            var event2Published = false;
            object received2 = null;

            Event.Subscribe((arg) => { event1Published = true; received1 = arg; });
            Event.Subscribe((arg) => { event2Published = true; received2 = arg; });
            Event.Publish(Payload);

            Assert.IsTrue(event1Published);
            Assert.AreSame(Payload, received1);

            Assert.IsTrue(event2Published);
            Assert.AreSame(Payload, received2);
        }

        [TestCase]
        public void SubscriptionsCanPassFilter()
        {
            var eventPublished = false;
            object received = null;

            Event.Subscribe((arg) => { eventPublished = true; received = arg; }, filter: (arg) => true);
            Event.Publish(Payload);

            Assert.IsTrue(eventPublished);
            Assert.AreSame(Payload, received);
        }

        [TestCase]
        public void FilterExcludesSubscriptions()
        {
            var eventPublished = false;
            object received = null;

            Event.Subscribe((arg) => { eventPublished = true; received = arg; }, filter: (arg) => false);
            Event.Publish(Payload);

            Assert.IsFalse(eventPublished);
            Assert.AreNotSame(Payload, received);
        }
    }
}
