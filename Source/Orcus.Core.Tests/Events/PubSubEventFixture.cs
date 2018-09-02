using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Orcus.Core.Tests.Mocks.Events;

namespace Orcus.Core.Tests.Events
{
    [TestFixture]
    public class PubSubEventFixture
    {
        private TestableMockPubSubEvent Event { get; set; }

        [SetUp]
        public void SetUp()
        {
            Event = new TestableMockPubSubEvent();
        }

        [TestCase]
        public void CanSubscribeAndPublish()
        {
            var eventPublished = false;

            Event.Subscribe(() => { eventPublished = true; });
            Event.Publish();

            Assert.IsTrue(eventPublished);
        }

        [TestCase]
        public void CanSubscribeAndPublishToMultipleSubscribers()
        {
            var event1Published = false;
            var event2Published = false;

            Event.Subscribe(() => { event1Published = true; });
            Event.Subscribe(() => { event2Published = true; });
            Event.Publish();

            Assert.IsTrue(event1Published);
            Assert.IsTrue(event2Published);
        }

        [TestCase]
        public void ThreadOptionPublisherThreadIsCalledOnPublisherThread()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext calledSynchronizationContext = null;

            Event.Subscribe(() => calledSynchronizationContext = SynchronizationContext.Current);
            Event.Publish();

            Assert.AreSame(SynchronizationContext.Current, calledSynchronizationContext);
        }

        [TestCase]
        public void ShouldNotExecuteGarbageCollectedSubscriptions()
        {
            var eventPublished = false;
            var mockContext = new MockContext(() => { eventPublished = true; });
            var weakReference = new WeakReference(mockContext);

            Event.Subscribe(mockContext.Action);
            mockContext = null;
            GC.Collect();
            Event.Publish();

            Assert.IsTrue(eventPublished);
            Assert.IsFalse(weakReference.IsAlive);
        }
    }
}
