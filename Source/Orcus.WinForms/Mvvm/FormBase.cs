using System;
using System.Windows.Forms;

namespace Orcus.WinForms.Mvvm
{
    // Can't be abstract if it should be used with WinForms designer.
    public class FormBase<T> : Form
    {
        public T ViewModel { get; set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            ViewModelLocator.AutoWireViewModel(this);

            SetupView();
            RegisterCallbacks();
        }

        protected virtual void SetupView() { }

        protected virtual void RegisterCallbacks() { }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            UnRegisterCallbacks();

            base.OnFormClosed(e);
        }

        protected virtual void UnRegisterCallbacks() { }
    }
}
