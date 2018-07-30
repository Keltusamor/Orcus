using System;
using System.Threading.Tasks;

namespace Orcus.Core.Events.Subscriptions
{
    class BackgroundEventSubscription : EventSubscription
    {
        public BackgroundEventSubscription(IDelegateReference actionReference)
            : base(actionReference)
        {
        }

        protected override void InvokeAction(Action action)
        {
            Task.Run(action);
        }
    }
}
