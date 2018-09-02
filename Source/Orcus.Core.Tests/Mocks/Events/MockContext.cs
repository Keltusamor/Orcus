using System;

namespace Orcus.Core.Tests.Mocks.Events
{
    public class MockContext
    {
        public Action Action { get; }

        public MockContext(Action action)
        {
            Action = action;
        }
    }
}
