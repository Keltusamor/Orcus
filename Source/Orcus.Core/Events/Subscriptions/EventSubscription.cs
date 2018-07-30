using System;

namespace Orcus.Core.Events.Subscriptions
{
    class EventSubscription : IEventSubscription
    {
        private IDelegateReference ActionReference { get; }

        public Action Action { get { return (Action)ActionReference.Target; } }

        public SubscriptionToken SubscriptionToken { get; set; }

        public EventSubscription(IDelegateReference actionReference)
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
