using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Orcus.WinForms.Mvvm;
using WinForms.Sandbox.ViewModels;

namespace WinForms.Sandbox.Views
{
    public partial class MainWindow : FormBase<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void RegisterCallbacks()
        {

        }

        protected override void SetupView()
        {

        }

        protected override void UnRegisterCallbacks()
        {

        }
    }
}
