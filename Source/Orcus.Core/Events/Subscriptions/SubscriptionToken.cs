using System;

namespace Orcus.Core.Events.Subscriptions
{
    public class SubscriptionToken : IEquatable<SubscriptionToken>, IDisposable
    {
        private Guid ID { get; }

        private Action<SubscriptionToken> UnSubscribeAction { get; set; }

        public SubscriptionToken(Action<SubscriptionToken> unSubscribeAction)
        {
            ID = Guid.NewGuid();
            UnSubscribeAction = unSubscribeAction;
        }

        public bool Equals(SubscriptionToken other)
        {
            if (other == null)
                return false;

            return Equals(ID, other.ID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as SubscriptionToken);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public void Dispose()
        {
            if (UnSubscribeAction != null)
            {
                UnSubscribeAction(this);
                UnSubscribeAction = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
