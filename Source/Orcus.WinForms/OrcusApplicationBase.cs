using System;
using System.Windows.Forms;
using Orcus.Core;
using Orcus.Core.Events;
using Orcus.Core.IoC;
using Orcus.Core.Mvvm;

namespace Orcus.WinForms
{
    public abstract class OrcusApplicationBase
    {
        private ContainerAdapter ContainerAdapter { get; set; }
        private Form MainWindow { get; set; }

        public virtual void Run()
        {
            BeforeInitialize();
            Initialize();
            AfterInitialize();
        }

        protected virtual void BeforeInitialize()
        {
            ViewModelFactory.RegisterConventionBasedViewModelFactory((view, viewModelType) => ContainerAdapter.Resolve(viewModelType));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        protected virtual void Initialize()
        {
            ContainerAdapter = CreateContainerAdapter() ?? throw new NullReferenceException("You have to provide an IContainerAdapter.");

            RegisterDependencies(ContainerAdapter);
            ContainerAdapter.FinishRegistration();

            ResolveDependencies(ContainerAdapter);

            MainWindow = CreateMainForm() ?? throw new NullReferenceException("Window was not initialized. Make sure to not return null in CreateMainWindow().");
            MainWindow.FormClosed += OnMainWindowDeleted;
        }

        protected abstract ContainerAdapter CreateContainerAdapter();

        protected virtual void RegisterDependencies(ContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(ContainerAdapter);
            containerRegistry.RegisterInstance<ContainerRegistry>(ContainerAdapter);
            containerRegistry.RegisterInstance<ContainerProvider>(ContainerAdapter);
            containerRegistry.RegisterSingleton<UiThreadDispatcherImpl, UiThreadDispatcherImpl>();
            containerRegistry.RegisterSingleton<EventAggregator, EventAggregatorImpl>();
        }

        protected virtual void ResolveDependencies(ContainerProvider containerProvider)
        {
        }

        protected abstract Form CreateMainForm();

        protected virtual void OnMainWindowDeleted(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected virtual void AfterInitialize()
        {
            var eventAggregator = ContainerAdapter.Resolve<EventAggregator>();
            // eventAggregator.GetEvent<OrcusInitializedEvent>().Publish();
            // MainWindow.Show();
            Application.Run(MainWindow);
        }
    }
}
