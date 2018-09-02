using Orcus.Core.IoC;

namespace Orcus.GtkSharp3.Unity
{
    public abstract class OrcusApplication : OrcusApplicationBase
    {
        protected sealed override ContainerAdapter CreateContainerAdapter()
        {
            return new UnityContainerAdapter();
        }
    }
}
