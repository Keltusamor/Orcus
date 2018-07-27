using System;
using Gtk;
using Orcus.GtkSharp3;

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

    class GtkSharp3SandboxApplication : OrcusApplicationBase
    {
        protected override Window CreateMainWindow()
        {
            return new MainWindow("GtkSharp3.Sandbox");
        }
    }
}
