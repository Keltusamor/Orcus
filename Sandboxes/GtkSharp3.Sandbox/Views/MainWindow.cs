using Gtk;
using GtkSharp3.Sandbox.ViewModels;
using Orcus.GtkSharp3.Mvvm;

namespace GtkSharp3.Sandbox.Views
{
    public class MainWindow : WindowBase<MainWindowViewModel>
    {
        private Button TestButton { get; set; }

        public MainWindow(string title) : base(title)
        {
        }

        protected override void SetupView()
        {
            var parent = new VBox();
            Add(parent);

            TestButton = new Button();
            parent.Add(TestButton);
            TestButton.Label = ViewModel.TestButtonText;
        }

        protected override void RegisterCallbacks()
        {
            TestButton.Clicked += ViewModel.OnClick;
        }

        protected override void UnRegisterCallbacks()
        {
            TestButton.Clicked -= ViewModel.OnClick;
        }
    }
}
