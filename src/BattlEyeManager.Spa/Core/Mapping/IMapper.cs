namespace BattlEyeManager.Spa.Core.Mapping
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}