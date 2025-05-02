namespace SupplyChain.IServiceContracts
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);
        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
