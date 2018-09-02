using Gtk;
using Orcus.Core;

namespace Orcus.GtkSharp3
{
    public sealed class UiThreadDispatcherImpl : UiThreadDispatcher
    {
        public void Invoke(System.Action action)
        {
            Application.Invoke((s, args) => action());
        }
    }
}
