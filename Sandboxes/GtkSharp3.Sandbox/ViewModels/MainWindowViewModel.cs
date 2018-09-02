using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GtkSharp3.Sandbox.Events;
using GtkSharp3.Sandbox.Services;
using Orcus.Core.Events;

namespace GtkSharp3.Sandbox.ViewModels
{
    public class MainWindowViewModel
    {
        private EventAggregator EventAggregator { get; }

        private ITestService TestService { get; }

        public string TestButtonText => "TestButton";

        public MainWindowViewModel(EventAggregator eventAggregator, ITestService testService)
        {
            TestService = testService;
            EventAggregator = eventAggregator;

            EventAggregator.GetEvent<TestEvent>().Subscribe(TestEventCallback);

            Console.WriteLine($"UI Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            EventAggregator.GetEvent<TestEventWithPayload>().Subscribe(TestEventCallbackWithArgumentOnPublisherThread);
            EventAggregator.GetEvent<TestEventWithPayload>().Subscribe(TestEventCallbackWithArgumentOnBackgroundThread, ThreadOption.BackgroundThread);
            EventAggregator.GetEvent<TestEventWithPayload>().Subscribe(TestEventCallbackWithArgumentOnUIThread, ThreadOption.UIThread);
        }

        public void OnClick(object sender, EventArgs args)
        {
            TestService.Print();
            EventAggregator.GetEvent<TestEvent>().Publish();

            Task.Run(() =>
            {
                Console.WriteLine($"Publisher Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                for (int i = 0; i < 10; i++)
                    EventAggregator.GetEvent<TestEventWithPayload>().Publish("EventAggregation the easy way.");
            });
        }

        public void TestEventCallback()
        {
            Console.WriteLine("Callback executed.");
        }

        public void TestEventCallbackWithArgumentOnPublisherThread(string payload)
        {
            Console.WriteLine($"Callback executed on publisher thread {Thread.CurrentThread.ManagedThreadId}. Payload is \"{payload}\".");
        }

        public void TestEventCallbackWithArgumentOnBackgroundThread(string payload)
        {
            Console.WriteLine($"Callback executed on background thread {Thread.CurrentThread.ManagedThreadId}. Payload is \"{payload}\".");
        }

        public void TestEventCallbackWithArgumentOnUIThread(string payload)
        {
            Console.WriteLine($"Callback executed on ui thread {Thread.CurrentThread.ManagedThreadId}. Payload is \"{payload}\".");
        }
    }
}
