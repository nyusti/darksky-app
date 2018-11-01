namespace DarkSky.Application.Injection
{
    /// <summary>
    /// Dependency resolver interface
    /// </summary>
    /// <seealso cref="DarkSky.Application.Injection.IDependencyScope"/>
    public interface IDependencyResolver : IDependencyScope
    {
        /// <summary>
        /// Begins the scope.
        /// </summary>
        /// <returns>The dependency scope.</returns>
        IDependencyScope BeginScope();
    }
}