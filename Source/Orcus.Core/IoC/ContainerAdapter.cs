namespace Orcus.Core.IoC
{
    public interface ContainerAdapter : ContainerRegistry, ContainerProvider
    {
        /// <summary>
        /// <remarks>Only containers that are mutale can support modules.</remarks>
        /// </summary>
        bool SupportsModules { get; }

        /// <summary>
        /// Can be used if additional steps need to be done after all depenencies are registered.
        /// </summary>
        void FinishRegistration();
    }
}
