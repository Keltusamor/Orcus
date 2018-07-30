using System;
using System.Threading;

namespace Orcus.Core.Events.Subscriptions
{
    class DispatchedEventSubscription : EventSubscription
    {
        private SynchronizationContext SynchronizationContext { get; }

        public DispatchedEventSubscription(IDelegateReference actionReference, SynchronizationContext synchronizationContext)
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
