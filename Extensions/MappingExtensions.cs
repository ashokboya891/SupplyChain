using AutoMapper;
using AutoMapper.QueryableExtensions;
using SupplyChain.IServiceContracts;
using SupplyChain.Models;

namespace SupplyChain.Extensions
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Maps a PaginatedList of TSource to PaginatedList of TDestination
        /// </summary>
        public static PaginatedList<TDestination> ToPaginatedList<TSource, TDestination>(
            this IMapper mapper,
            PaginatedList<TSource> source)
        {
            if (source == null)
                return null;

            var mappedItems = mapper.Map<List<TDestination>>(source.Items);

            return new PaginatedList<TDestination>(
                mappedItems,
                source.TotalCount,
                source.PageIndex,
                source.PageSize);
        }

        /// <summary>
        /// Projects queryable source to destination type using the provided mapping service
        /// </summary>
        public static IQueryable<TDestination> ProjectTo<TDestination>(
            this IQueryable source,
            IMappingService mappingService)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (mappingService == null)
                throw new ArgumentNullException(nameof(mappingService));

            return mappingService.ProjectTo<TDestination>(source);
        }

        /// <summary>
        /// Alternative version that works directly with IMapper
        /// </summary>
        public static IQueryable<TDestination> ProjectTo<TDestination>(
            this IQueryable source,
            IMapper mapper)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            return source.ProjectTo<TDestination>(mapper.ConfigurationProvider);
        }
    }

}
