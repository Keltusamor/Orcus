using System.Threading;

namespace Orcus.Core.Tests.Mocks.Events.Subscriptions
{
    public class MockSynchronizationContext : SynchronizationContext
    {
        public bool WasPostCalled { get; private set; }

        public object StatePostWasCalledWith { get; private set; }

        public override void Post(SendOrPostCallback callback, object state)
        {
            WasPostCalled = true;
            StatePostWasCalledWith = state;
        }
    }
}
