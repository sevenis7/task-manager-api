namespace TaskManager.Services.Mappers.Interfaces
{
    public interface IEntityMapper<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        TDto MapToDto(TEntity entity);
    }
}
