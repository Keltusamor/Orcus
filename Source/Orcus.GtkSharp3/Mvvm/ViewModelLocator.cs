using Orcus.Core.Mvvm;

namespace Orcus.GtkSharp3.Mvvm
{
    public static class ViewModelLocator
    {
        public static void AutoWireViewModel<T>(WindowBase<T> view)
        {
            var viewModel = ViewModelFactory.Create(view, typeof(T));
            BindViewModel<T>(view, viewModel);
        }

        private static void BindViewModel<T>(object view, object viewModel)
        {
            if (view is WindowBase<T> element)
                element.ViewModel = (T)viewModel;
        }
    }
}
