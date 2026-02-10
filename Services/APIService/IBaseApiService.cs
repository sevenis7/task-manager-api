
namespace TaskManager.Services.APIService
{
    public interface IBaseApiService<TCreateModel, TDto>
        where TCreateModel : class
        where TDto : class
    {
        Task<TDto> AddAsync(TCreateModel model);
        Task DeleteAsync(int id);
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
    }
}