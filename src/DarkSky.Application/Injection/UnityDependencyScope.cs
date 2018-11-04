namespace DarkSky.Application.Injection
{
    using System;
    using System.Collections.Generic;
    using Unity;
    using Unity.Exceptions;

    /// <summary>
    /// Unity based dependency scope
    /// </summary>
    /// <seealso cref="DarkSky.Application.Injection.IDependencyScope"/>
    internal sealed class UnityDependencyScope : IDependencyScope
    {
        private readonly IUnityContainer unityContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyScope"/> class.
        /// </summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <exception cref="ArgumentNullException">unityContainer is null</exception>
        public UnityDependencyScope(IUnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                throw new ArgumentNullException(nameof(unityContainer));
            }

            this.unityContainer = unityContainer.CreateChildContainer();
        }

        /// <inheritdoc/>
        public object BuildUp(Type serviceType, object existingObject)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (existingObject == null)
            {
                throw new ArgumentNullException(nameof(existingObject));
            }

            return this.unityContainer.BuildUp(serviceType, existingObject);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.unityContainer.Dispose();
        }

        /// <inheritdoc/>
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            try
            {
                return this.unityContainer.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // TODO: log exception
                return null;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return this.unityContainer.ResolveAll(serviceType);
        }
    }
}