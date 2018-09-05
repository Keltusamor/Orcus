using System;
using System.Collections.Generic;
using System.Text;
using Gtk;
using GtkSharp3.Sandbox.ViewModels;
using Orcus.GtkSharp3.Mvvm;

namespace GtkSharp3.Sandbox.Views
{
    public sealed class SomeWidget : FrameBase<SomeWidgetViewModel>
    {
        public SomeWidget()
            : base("Frame title")
        {
        }

        protected override void SetupView()
        {
            var label = new Label("Some text");
            Add(label);
        }

        protected override void RegisterCallbacks()
        {

        }

        protected override void UnRegisterCallbacks()
        {

        }
    }
}
