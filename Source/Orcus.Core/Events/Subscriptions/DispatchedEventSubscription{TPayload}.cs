using System;
using System.Threading;

namespace Orcus.Core.Events.Subscriptions
{
    class DispatchedEventSubscription<TPayload> : PublisherEventSubscription<TPayload>
    {
        private SynchronizationContext SynchronizationContext { get; }

        public DispatchedEventSubscription(DelegateReference actionReference, DelegateReference filterReference, SynchronizationContext synchronizationContext)
            : base(actionReference, filterReference)
        {
            SynchronizationContext = synchronizationContext;
        }

        protected override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            SynchronizationContext.Post((obj) => action((TPayload)obj), argument);
        }
    }
}
