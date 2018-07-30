using System;

namespace Orcus.Core.Events
{
    interface IDelegateReference
    {
        Delegate Target { get; }
    }
}
