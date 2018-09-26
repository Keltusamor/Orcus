using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Orcus.Core.Events;
using Orcus.Core.Events.Subscriptions;
using Orcus.Core.Tests.Mocks.Events;
using Orcus.Core.Tests.Mocks.Events.Subscriptions;

namespace Orcus.Core.Tests.Events.Subscriptions
{
    [TestFixture]
    public class DispatchedEventSubscriptionFixture
    {
        [TestCase]
        public void DelegateIsCalledOnDispatcher()
        {
            var delegateReference = new MockDelegateReference((Action)(() => { }));
            var synchronizationContext = new MockSynchronizationContext();
            var eventSubscription = new DispatchedEventSubscription(delegateReference, synchronizationContext);

            eventSubscription.GetSubscriptionCallback().Invoke(null);

            Assert.IsTrue(synchronizationContext.WasPostCalled);
        }

        [TestCase]
        public void DelegateIsCalledOnDispatcher_Generic()
        {
            var delegateReference = new MockDelegateReference((Action<object>)((arg) => { }));
            var filterDelegateReference = new MockDelegateReference((Predicate<object>)(arg => true));
            var synchronizationContext = new MockSynchronizationContext();
            var eventSubscription = new DispatchedEventSubscription<object>(delegateReference, filterDelegateReference, synchronizationContext);

            eventSubscription.GetSubscriptionCallback().Invoke(null);

            Assert.IsTrue(synchronizationContext.WasPostCalled);
        }

        [TestCase]
        public void FilterPreventsFromDelegateBeingCalled()
        {
            var delegateReference = new MockDelegateReference((Action<object>)((arg) => { }));
            var filterDelegateReference = new MockDelegateReference((Predicate<object>)(arg => false));
            var synchronizationContext = new MockSynchronizationContext();
            var eventSubscription = new DispatchedEventSubscription<object>(delegateReference, filterDelegateReference, synchronizationContext);

            eventSubscription.GetSubscriptionCallback().Invoke(null);

            Assert.IsFalse(synchronizationContext.WasPostCalled);
        }
    }
}
