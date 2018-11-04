namespace DarkSky.Application.Mapping
{
    /// <summary>
    /// Domain model mapper interface
    /// </summary>
    public interface IDomainModelMapper
    {
        /// <summary>
        /// Maps the specified source to the domain model.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The domain model.</returns>
        TDestination Map<TDestination>(object source);
    }
}