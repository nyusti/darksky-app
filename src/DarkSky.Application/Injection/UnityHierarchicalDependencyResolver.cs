namespace DarkSky.Application.Injection
{
    using System;
    using System.Collections.Generic;
    using Unity;
    using Unity.Exceptions;

    /// <summary>
    /// Unity based hierarchical dependency resolver
    /// </summary>
    /// <seealso cref="DarkSky.Application.Injection.IDependencyResolver"/>
    public class UnityHierarchicalDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer unityContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityHierarchicalDependencyResolver"/> class.
        /// </summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <exception cref="ArgumentNullException">unityContainer is null</exception>
        public UnityHierarchicalDependencyResolver(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        /// <inheritdoc/>
        public IDependencyScope BeginScope()
        {
            return new UnityDependencyScope(this.unityContainer);
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
            this.Dispose(true);
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

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return this.unityContainer.ResolveAll(serviceType);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.unityContainer.Dispose();
            }
        }
    }
}