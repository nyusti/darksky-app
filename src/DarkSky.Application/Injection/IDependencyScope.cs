namespace DarkSky.Application.Injection
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Dependency scope interface
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    public interface IDependencyScope : IDisposable
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The resolved service</returns>
        object GetService(Type serviceType);

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The resolved services</returns>
        IEnumerable<object> GetServices(Type serviceType);

        /// <summary>
        /// Runs the object to the container.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="existingObject">The existing object.</param>
        /// <returns>The built up object</returns>
        object BuildUp(Type serviceType, object existingObject);
    }
}