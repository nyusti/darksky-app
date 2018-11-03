namespace DarkSky.Application.Mapping
{
    public interface IDomainModelMapper
    {
        TDestination Map<TDestination>(object source);
    }
}