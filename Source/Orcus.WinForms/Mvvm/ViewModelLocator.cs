using System.ComponentModel;
using Orcus.Core.Mvvm;

namespace Orcus.WinForms.Mvvm
{
    public static class ViewModelLocator
    {
        public static void AutoWireViewModel<T>(FormBase<T> view)
        {
            if (!(System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv"))
            {
                var viewModel = ViewModelFactory.Create(view, typeof(T));
                BindViewModel<T>(view, viewModel);
            }
        }

        private static void BindViewModel<T>(object view, object viewModel)
        {
            if (view is FormBase<T> window)
                window.ViewModel = (T)viewModel;
        }
    }
}
