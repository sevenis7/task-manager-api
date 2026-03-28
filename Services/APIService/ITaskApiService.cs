using TaskManager.Models;

namespace TaskManager.Services.APIService
{
    public interface ITaskApiService
    {
        Task<TaskDto> AddAsync(CreateTaskModel model, int userId);
        Task<TaskDto> ChangeStatusAsync(int id, int statusId, int userId);
        Task DeleteAsync(int id, int userId);
        Task<TaskDto> EditTaskAsync(int id, UpdateTaskModel model, int userId);
        Task<TaskDto> GetByIdAsync(int id, int userId);
        Task<PagedResponse<TaskDto>> GetTasksAsync(int userId, int page = 1, int pageSize = 10, bool includeExpired = false, bool onlyExpired = false, int? categoryId = null, int? statusId = null);
    }
}