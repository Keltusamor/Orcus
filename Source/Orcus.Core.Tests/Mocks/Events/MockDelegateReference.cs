using System;
using Orcus.Core.Events;

namespace Orcus.Core.Tests.Mocks.Events
{
    public class MockDelegateReference : DelegateReference
    {
        public Delegate Target { get; set; }

        public MockDelegateReference(Delegate target)
        {
            Target = target;
        }
    }
}
