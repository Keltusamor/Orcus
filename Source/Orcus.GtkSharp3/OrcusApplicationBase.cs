using System;
using Gtk;
using Orcus.Core.Events;
using Orcus.Core.IoC;
using Orcus.Core.Mvvm;

namespace Orcus.GtkSharp3
{
    public abstract class OrcusApplicationBase
    {
        private Application Application { get; set; }
        private IContainerAdapter ContainerAdapter { get; set; }
        private Window MainWindow { get; set; }

        public virtual void Run()
        {
            BeforeInitialize();
            Initialize();
            AfterInitialize();
        }

        protected virtual void BeforeInitialize()
        {
            ViewModelFactory.RegisterConventionBasedViewModelFactory((view, viewModelType) => ContainerAdapter.Resolve(viewModelType));

            Application.Init();
        }

        protected virtual void Initialize()
        {
            ContainerAdapter = CreateContainerAdapter() ?? throw new NullReferenceException("You have to provide an IContainerAdapter.");

            RegisterDependencies(ContainerAdapter);
            ContainerAdapter.FinishRegistration();

            ResolveDependencies(ContainerAdapter);

            MainWindow = CreateMainWindow() ?? throw new NullReferenceException("Window was not initialized. Make sure to not return null in CreateMainWindow().");
            MainWindow.DeleteEvent += OnMainWindowDeleted;
        }

        protected abstract IContainerAdapter CreateContainerAdapter();

        protected virtual void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(ContainerAdapter);
            containerRegistry.RegisterInstance<IContainerRegistry>(ContainerAdapter);
            containerRegistry.RegisterInstance<IContainerProvider>(ContainerAdapter);
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
        }

        protected virtual void ResolveDependencies(IContainerProvider containerProvider)
        {
        }

        protected abstract Window CreateMainWindow();

        protected virtual void OnMainWindowDeleted(object sender, EventArgs e)
        {
            Application.Quit();
        }

        protected virtual void AfterInitialize()
        {
            MainWindow.Show();
            Application.Run();
        }
    }
}
