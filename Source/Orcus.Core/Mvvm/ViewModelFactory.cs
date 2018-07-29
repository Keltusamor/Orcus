using System;
using System.Collections.Generic;

namespace Orcus.Core.Mvvm
{
    public static class ViewModelFactory
    {
        private static Dictionary<Type, Func<object>> CustomViewModelFactories { get; }

        private static Func<object, Type, object> ConventionBasedViewModelFactory { get; set; }

        static ViewModelFactory()
        {
            CustomViewModelFactories = new Dictionary<Type, Func<object>>();
            ConventionBasedViewModelFactory = (view, viewModelType) => Activator.CreateInstance(viewModelType);
        }

        public static void RegisterCustomViewModelFactory(Type viewType, Func<object> viewModelFactory)
        {
            CustomViewModelFactories.Add(viewType, viewModelFactory);
        }

        public static void RegisterCustomViewModelFactory<TView>(Func<object> viewModelFactory)
        {
            CustomViewModelFactories.Add(typeof(TView), viewModelFactory);
        }

        public static void RegisterConventionBasedViewModelFactory(Func<object, Type, object> viewModelFactory)
        {
            ConventionBasedViewModelFactory = viewModelFactory;
        }

        public static object Create(object view, Type viewModelType = null)
        {
            object viewModel = null;

            if (TryCreateViewModelWithCustomFactory(view, out viewModel))
                return viewModel;

            if (viewModelType == null)
                viewModelType = ViewModelLocationProvider.LocateViewModelType(view);

            if (TryCreateViewModelWithConventionBasedFactory(view, viewModelType, out viewModel))
                return viewModel;

            throw new InvalidOperationException($"ViewModel for view: {view} could not be created. Make sure that you are registering the correct dependencies for the ViewModel in RegisterDependencies(IContainerRegistry).");
        }

        private static bool TryCreateViewModelWithCustomFactory(object view, out object viewModel)
        {
            viewModel = null;
            var viewType = view.GetType();
            if (CustomViewModelFactories.ContainsKey(viewType))
                viewModel = CustomViewModelFactories[viewType]();
            return viewModel != null;
        }

        private static bool TryCreateViewModelWithConventionBasedFactory(object view, Type viewModelType, out object viewModel)
        {
            viewModel = null;
            viewModel = ConventionBasedViewModelFactory(view, viewModelType);
            return viewModel != null;
        }
    }
}
