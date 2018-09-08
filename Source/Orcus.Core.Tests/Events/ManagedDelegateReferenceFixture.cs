using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Orcus.Core.Events;

namespace Orcus.Core.Tests.Events
{
    [TestFixture]
    public class ManagedDelegateReferenceFixture
    {
        [TestCase]
        public async Task When_KeepReferenceAlive_False_Then_DelegateShouldGetGarbageCollected()
        {
            var someClassHandler = new SomeClassHandler();
            var delegateReference = new ManagedDelegateReference((Action<string>)someClassHandler.Do, false);

            someClassHandler = null;
            // Delay is important here. If we do GC.Collect() immediately the delegate is not listed for garbage collection yet.
            await Task.Delay(100);
            GC.Collect();

            Assert.IsNull(delegateReference.Target);
        }

        [TestCase]
        public void When_KeepReferenceAlive_False_Then_StaticDelegatesShouldGetInvoked()
        {
            var delegateReference = new ManagedDelegateReference((Action<string>)SomeClassHandler.StaticDo, false);
            Assert.IsNotNull(delegateReference.Target);
        }

        [TestCase]
        public async Task When_KeepReferenceAlive_True_Then_DelegateShouldNotGetGarbageCollected()
        {
            var someClassHandler = new SomeClassHandler();
            var delegateReference = new ManagedDelegateReference((Action<string>)someClassHandler.Do, true);

            someClassHandler = null;
            // Delay is important here. If we do GC.Collect() immediately the delegate is not listed for garbage collection yet.
            await Task.Delay(100);
            GC.Collect();

            Assert.IsNotNull(delegateReference.Target);
        }

        [TestCase]
        public void TargetShouldReturnAction()
        {
            var someClassHandler = new SomeClassHandler();
            var delegateReference = new ManagedDelegateReference((Action<string>)someClassHandler.Do, false);

            ((Action<string>)delegateReference.Target)("Payload");

            Assert.AreEqual("Payload", someClassHandler.Arg);
        }

        public class SomeClassHandler
        {
            public string Arg { get; set; }

            public static void StaticDo(string value)
            {
            }

            public void Do(string value)
            {
                Arg = value;
            }
        }
    }
}
