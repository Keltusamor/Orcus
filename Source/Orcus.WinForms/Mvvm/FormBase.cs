using System;
using System.Windows.Forms;

namespace Orcus.WinForms.Mvvm
{
    public abstract class FormBase<T> : Form
    {
        public T ViewModel { get; set; }

        protected FormBase()
            : base()
        {
        }

        protected FormBase(string title)
            : base()
        {
            Text = title;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            ViewModelLocator.AutoWireViewModel(this);

            SetupView();
            RegisterCallbacks();
        }

        protected abstract void SetupView();

        protected abstract void RegisterCallbacks();

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            UnRegisterCallbacks();

            base.OnFormClosed(e);
        }

        protected abstract void UnRegisterCallbacks();
    }
}
