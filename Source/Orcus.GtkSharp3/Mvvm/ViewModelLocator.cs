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

        public static void AutoWireViewModel<T>(FrameBase<T> view)
        {
            var viewModel = ViewModelFactory.Create(view, typeof(T));
            BindViewModel<T>(view, viewModel);
        }

        public static void AutoWireViewModel<T>(HBoxBase<T> view)
        {
            var viewModel = ViewModelFactory.Create(view, typeof(T));
            BindViewModel<T>(view, viewModel);
        }

        public static void AutoWireViewModel<T>(VBoxBase<T> view)
        {
            var viewModel = ViewModelFactory.Create(view, typeof(T));
            BindViewModel<T>(view, viewModel);
        }

        public static void AutoWireViewModel<T>(PanedBase<T> view)
        {
            var viewModel = ViewModelFactory.Create(view, typeof(T));
            BindViewModel<T>(view, viewModel);
        }

        private static void BindViewModel<T>(object view, object viewModel)
        {
            if (view is WindowBase<T> window)
                window.ViewModel = (T)viewModel;

            if (view is FrameBase<T> frame)
                frame.ViewModel = (T)viewModel;

            if (view is HBoxBase<T> hBox)
                hBox.ViewModel = (T)viewModel;

            if (view is VBoxBase<T> vBox)
                vBox.ViewModel = (T)viewModel;

            if (view is PanedBase<T> paned)
                paned.ViewModel = (T)viewModel;
        }
    }
}
