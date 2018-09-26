using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Orcus.Core.IoC;
using Orcus.WinForms.Unity;
using WinForms.Sandbox.Services;
using WinForms.Sandbox.Views;

namespace WinForms.Sandbox
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            new WinFormsSandbox().Run();
        }
    }

    internal sealed class WinFormsSandbox : OrcusApplication
    {
        protected override Form CreateMainForm()
        {
            return new MainWindow();
        }

        protected override void RegisterDependencies(ContainerRegistry containerRegistry)
        {
            base.RegisterDependencies(containerRegistry);

            containerRegistry.Register<LogService>();
        }
    }
}
