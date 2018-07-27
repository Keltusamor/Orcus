using System;
using System.Collections.Generic;
using System.Text;
using Gtk;
using Orcus.Core.IoC;

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
            Application.Init();
            Application = new Application("org.Orcus.GtkSharp3", GLib.ApplicationFlags.None);
            Application.Register(GLib.Cancellable.Current);
        }

        protected virtual void Initialize()
        {
            ContainerAdapter = CreateContainerAdapter();
            RegisterDependencies(ContainerAdapter);
            ContainerAdapter.FinishRegistration();

            MainWindow = CreateMainWindow() ?? throw new NullReferenceException("Window was not initialized. Make sure to not return null in CreateMainWindow().");
            MainWindow.DeleteEvent += OnMainWindowDeleted;
            Application.AddWindow(MainWindow);
        }

        protected abstract IContainerAdapter CreateContainerAdapter();

        protected virtual void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(typeof(IContainerAdapter), ContainerAdapter);
            containerRegistry.RegisterInstance(typeof(IContainerRegistry), ContainerAdapter);
            containerRegistry.RegisterInstance(typeof(IContainerProvider), ContainerAdapter);
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
