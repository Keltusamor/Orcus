using Gtk;
using GtkSharp3.Sandbox.ViewModels;
using Orcus.GtkSharp3.Mvvm;

namespace GtkSharp3.Sandbox.Views
{
    public sealed class MainWindow : WindowBase<MainWindowViewModel>
    {
        public MainWindow(string title) : base(title)
        {
        }

        protected override void SetupView()
        {
            var parent = new VBox();
            Add(parent);
        }

        protected override void RegisterCallbacks()
        {
        }

        protected override void UnRegisterCallbacks()
        {
        }
    }
}
