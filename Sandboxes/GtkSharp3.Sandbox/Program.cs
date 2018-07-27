using System;
using Gtk;
using GtkSharp3.Sandbox.Services;
using GtkSharp3.Sandbox.Views;
using Orcus.Core.IoC;
using Orcus.GtkSharp3.Unity;

namespace GtkSharp3.Sandbox
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new GtkSharp3SandboxApplication().Run();
        }
    }

    class GtkSharp3SandboxApplication : OrcusApplication
    {
        protected override Window CreateMainWindow()
        {
            return new MainWindow("GtkSharp3.Sandbox");
        }

        protected override void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            base.RegisterDependencies(containerRegistry);

            containerRegistry.RegisterSingleton<ITestService, TestService>();
        }
    }
}
