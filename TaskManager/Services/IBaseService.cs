
namespace TaskManager.Services
{
    public interface IBaseService<TEntity, TCreateModel>
        where TEntity : class
        where TCreateModel : class
    {
        Task<TEntity> AddAsync(TCreateModel model);
        Task DeleteAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdASync(int id);
    }
}