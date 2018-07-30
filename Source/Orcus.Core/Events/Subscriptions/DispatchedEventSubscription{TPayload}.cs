using System;
using System.Threading;

namespace Orcus.Core.Events.Subscriptions
{
    class DispatchedEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        private SynchronizationContext SynchronizationContext { get; }

        public DispatchedEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference, SynchronizationContext synchronizationContext)
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
