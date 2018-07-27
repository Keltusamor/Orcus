using System;
using System.Collections.Generic;
using System.Text;
using Gtk;

namespace Orcus.GtkSharp3
{
    public abstract class OrcusApplicationBase
    {
        private Application Application { get; set; }
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
            MainWindow = CreateMainWindow() ?? throw new NullReferenceException("Window was not initialized. Make sure to not return null in CreateMainWindow().");
            MainWindow.DeleteEvent += OnMainWindowDeleted;
            Application.AddWindow(MainWindow);
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
