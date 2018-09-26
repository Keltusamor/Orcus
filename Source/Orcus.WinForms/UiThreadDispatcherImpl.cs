using System;
using Orcus.Core;

namespace Orcus.WinForms
{
    public sealed class UiThreadDispatcherImpl : UiThreadDispatcher
    {
        public void Invoke(Action action)
            => action();

        public void Invoke<TResult>(Func<TResult> func)
            => func();
    }
}
