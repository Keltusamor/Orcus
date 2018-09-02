using System;
using Orcus.Core;
using Orcus.Core.Events;

namespace GtkSharp3.Sandbox.ViewModels
{
    public sealed class MainWindowViewModel
    {
        private EventAggregator EventAggregator { get; }

        public MainWindowViewModel(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }
    }
}
