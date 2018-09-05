using Gtk;

namespace Orcus.GtkSharp3.Mvvm
{
    public abstract class VBoxBase<T> : VBox
    {
        public T ViewModel { get; set; }

        protected VBoxBase()
        {
        }

        protected override void OnShown()
        {
            base.OnShown();

            ViewModelLocator.AutoWireViewModel(this);

            SetupView();
            RegisterCallbacks();

            ShowAll();
        }

        protected abstract void SetupView();

        protected abstract void RegisterCallbacks();

        protected override void OnDestroyed()
        {
            UnRegisterCallbacks();

            base.OnDestroyed();
        }

        protected abstract void UnRegisterCallbacks();
    }
}
