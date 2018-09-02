using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Orcus.Core.Events;

namespace Orcus.Core.Tests.Events
{
    [TestFixture]
    public class ManagedDelegateReferenceFixture
    {
        [TestCase]
        public void AllowsDelegatesToBeGarbageCollected()
        {
            WeakReference test = null;
            ManagedDelegateReference delegateReference = null;

            {
                //var @delegate = new SomeClassHandler();
                //test = new WeakReference(@delegate);
                //delegateReference = new ManagedDelegateReference((Action<string>)((SomeClassHandler)@delegate).Do, false);
                //@delegate = null;

                test = new WeakReference(new SomeClassHandler());
                delegateReference = new ManagedDelegateReference((Action<string>)((SomeClassHandler)test.Target).Do, false);
                test.Target = null;
            }

            GC.Collect();

            Assert.Null(test.Target);
        }

        [TestCase]
        public void KeepSubscriberReferenceAlivePreventsGarbageCollectionOfDelegate()
        {

        }

        public class SomeClassHandler
        {
            public string Arg { get; set; }

            public void Do(string value)
            {

            }
        }
    }
}
