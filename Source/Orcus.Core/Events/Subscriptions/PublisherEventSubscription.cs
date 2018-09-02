using System;

namespace Orcus.Core.Events.Subscriptions
{
    internal class PublisherEventSubscription : EventSubscription
    {
        private DelegateReference ActionReference { get; }

        public Action Action { get { return (Action)ActionReference.Target; } }

        public SubscriptionToken SubscriptionToken { get; set; }

        public PublisherEventSubscription(DelegateReference actionReference)
        {
            ActionReference = actionReference ?? throw new ArgumentNullException(nameof(actionReference));
            if (!(actionReference.Target is Action))
                throw new ArgumentException($"Action reference has to be an {typeof(Action).FullName}.", nameof(actionReference));
        }

        public Action<object> GetSubscriptionCallback()
        {
            if (Action == null)
                return null;

            return payload =>
            {
                InvokeAction(Action);
            };
        }

        protected virtual void InvokeAction(Action action)
        {
            action();
        }
    }
}
