using System;
using System.Threading;

namespace Orcus.Core.Events.Subscriptions
{
    class DispatchedEventSubscription : PublisherEventSubscription
    {
        private SynchronizationContext SynchronizationContext { get; }

        public DispatchedEventSubscription(DelegateReference actionReference, SynchronizationContext synchronizationContext)
            : base(actionReference)
        {
            SynchronizationContext = synchronizationContext;
        }

        protected override void InvokeAction(Action action)
        {
            SynchronizationContext.Post((obj) => action(), null);
        }
    }
}
