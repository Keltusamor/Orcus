using NUnit.Framework;
using Orcus.Core.Events;
using Orcus.Core.Tests.Mocks.Events;

namespace Orcus.Core.Tests.Events
{
    [TestFixture]
    public class EventAggregatorFixture
    {
        [TestCase]
        public void GetReturnsSingletonForEvents()
        {
            var eventAggregator = new EventAggregatorImpl();

            var event1 = eventAggregator.GetEvent<SimpleMockEventBase>();
            var event2 = eventAggregator.GetEvent<SimpleMockEventBase>();

            Assert.AreSame(event1, event2);
        }
    }
}
