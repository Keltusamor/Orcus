using System;

namespace Orcus.Core
{
    public interface UiThreadDispatcher
    {
        void Invoke(Action action);
    }
}
