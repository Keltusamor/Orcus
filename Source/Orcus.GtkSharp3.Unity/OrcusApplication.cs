using Orcus.Core.IoC;

namespace Orcus.GtkSharp3.Unity
{
    public abstract class OrcusApplication : OrcusApplicationBase
    {
        protected sealed override IContainerAdapter CreateContainerAdapter()
        {
            return new UnityContainerAdapter();
        }
    }
}
