using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.Mappers
{
    public static class EntityMapperExtensions
    {
        public static List<TDestination> MapCollectionToDto<TSource, TDestination>(
            this IEntityMapper<TSource, TDestination> mapper,
            IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            return source.Select(mapper.MapToDto).ToList();
        }
    }
}
