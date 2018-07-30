using System;

namespace Orcus.Core.Events.Subscriptions
{
    class EventSubscription<TPayload> : IEventSubscription
    {
        private IDelegateReference ActionReference { get; }

        private IDelegateReference FilterReference { get; }

        public Action<TPayload> Action { get { return (Action<TPayload>)ActionReference.Target; } }

        public Predicate<TPayload> Filter { get { return (Predicate<TPayload>)FilterReference.Target; } }

        public SubscriptionToken SubscriptionToken { get; set; }

        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            ActionReference = actionReference ?? throw new ArgumentNullException(nameof(actionReference));
            FilterReference = filterReference ?? throw new ArgumentNullException(nameof(filterReference));

            if (!(actionReference.Target is Action<TPayload>))
                throw new ArgumentException($"Action reference has to be an {typeof(Action<TPayload>).FullName}.", nameof(actionReference));

            if (!(filterReference.Target is Predicate<TPayload>))
                throw new ArgumentException($"Fiter reference has to be an {typeof(Predicate<TPayload>).FullName}.", nameof(filterReference));
        }

        public Action<object> GetSubscriptionCallback()
        {
            if (Action == null && Filter == null)
                return null;

            return payload =>
            {
                if (Filter((TPayload)payload))
                    InvokeAction(Action, (TPayload)payload);
            };
        }

        protected virtual void InvokeAction(Action<TPayload> action, TPayload payload)
        {
            action(payload);
        }
    }
}
