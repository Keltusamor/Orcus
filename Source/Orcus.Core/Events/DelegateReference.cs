using System;

namespace Orcus.Core.Events
{
    internal interface DelegateReference
    {
        Delegate Target { get; }
    }
}
