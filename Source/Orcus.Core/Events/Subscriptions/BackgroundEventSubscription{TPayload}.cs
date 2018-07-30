using System;
using System.Threading.Tasks;

namespace Orcus.Core.Events.Subscriptions
{
    class BackgroundEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        public BackgroundEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
            : base(actionReference, filterReference)
        {
        }

        protected override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            Task.Run(() => action(argument));
        }
    }
}
