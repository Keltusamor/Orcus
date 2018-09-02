using System;
using System.Reflection;

namespace Orcus.Core.Events
{
    class ManagedDelegateReference : IDelegateReference
    {
        private WeakReference WeakReference { get; }
        private MethodInfo MethodInfo { get; }
        private Type DelegateType { get; }

        private Delegate target;
        public Delegate Target
        {
            get
            {
                if (target != null)
                    return target;

                return CreateDelegateViaReflection();
            }
            set { target = value; }
        }

        public ManagedDelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null)
                throw new ArgumentException(nameof(@delegate));

            if (keepReferenceAlive)
            {
                Target = @delegate;
            }
            else
            {
                WeakReference = new WeakReference(@delegate.Target);
                MethodInfo = @delegate.GetMethodInfo();
                DelegateType = @delegate.GetType();
            }
        }

        private Delegate CreateDelegateViaReflection()
        {
            if (MethodInfo.IsStatic)
                return MethodInfo.CreateDelegate(DelegateType, null);

            return MethodInfo.CreateDelegate(DelegateType, WeakReference.Target);
        }
    }
}
