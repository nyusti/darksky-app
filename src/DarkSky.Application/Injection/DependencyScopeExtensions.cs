namespace DarkSky.Application.Injection
{
    /// <summary>
    /// Dependency scope extensions
    /// </summary>
    public static class DependencyScopeExtensions
    {
        /// <summary>
        /// Resolves the specified dependency scope.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="dependencyScope">The dependency scope.</param>
        /// <returns>The resolved service</returns>
        public static T Resolve<T>(this IDependencyScope dependencyScope)
        {
            return (T)dependencyScope.GetService(typeof(T));
        }

        /// <summary>
        /// Runs the object through the container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependencyScope">The dependency scope.</param>
        /// <param name="existingObject">The existing object.</param>
        /// <returns>The built up object</returns>
        public static T BuildUp<T>(this IDependencyScope dependencyScope, T existingObject)
        {
            return (T)dependencyScope.BuildUp(typeof(T), existingObject);
        }
    }
}