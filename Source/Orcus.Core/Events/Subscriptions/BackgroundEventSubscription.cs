using System;
using System.Threading.Tasks;

namespace Orcus.Core.Events.Subscriptions
{
    class BackgroundEventSubscription : PublisherEventSubscription
    {
        public BackgroundEventSubscription(DelegateReference actionReference)
            : base(actionReference)
        {
        }

        protected override void InvokeAction(Action action)
        {
            Task.Run(action);
        }
    }
}
