using System;
using Gtk;
using GtkSharp3.Sandbox.Views;
using Orcus.Core.IoC;
using Orcus.GtkSharp3.Unity;

namespace GtkSharp3.Sandbox
{
    internal sealed class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new EmptyApplication().Run();
        }
    }

    internal sealed class EmptyApplication : OrcusApplication
    {
        protected override void BeforeInitialize()
        {
            base.BeforeInitialize();
        }

        protected override Window CreateMainWindow()
        {
            return new MainWindow("MainWindow");
        }

        protected override void RegisterDependencies(ContainerRegistry containerRegistry)
        {
            base.RegisterDependencies(containerRegistry);
        }
    }
}
