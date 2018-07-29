using System;
using System.Collections.Generic;
using System.Text;
using GtkSharp3.Sandbox.Services;

namespace GtkSharp3.Sandbox.ViewModels
{
    public class MainWindowViewModel
    {
        private ITestService TestService { get; }

        public string TestButtonText => "TestButton";

        public MainWindowViewModel(ITestService testService)
        {
            TestService = testService;
        }

        public void OnClick(object sender, EventArgs args)
        {
            TestService.Print();
        }
    }
}
