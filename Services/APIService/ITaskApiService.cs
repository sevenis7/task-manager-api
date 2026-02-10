using TaskManager.Models;

namespace TaskManager.Services.APIService
{
    public interface ITaskApiService
    {
        Task<TaskDto> AddAsync(CreateTaskModel model);
        Task<TaskDto> ChangeStatusAsync(int id, int statusId);
        Task DeleteAsync(int id);
        Task<TaskDto> EditTaskAsync(int id, UpdateTaskModel model);
        Task<TaskDto?> GetByIdAsync(int id);
        Task<List<TaskDto>> GetTasksAsync(bool includeExpired, bool onlyExpired, int? categoryId, int? statusId);
    }
}