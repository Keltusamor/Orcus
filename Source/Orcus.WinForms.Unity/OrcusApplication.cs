using Orcus.Core.IoC;

namespace Orcus.WinForms.Unity
{
    public abstract class OrcusApplication : OrcusApplicationBase
    {
        protected sealed override ContainerAdapter CreateContainerAdapter()
        {
            return new UnityContainerAdapter();
        }
    }
}
