using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<TaskItem> AddAsync(CreateTaskModel model, int userId);
        Task<TaskItem> ChangeStatusAsync(int taskId, int statusId, int userId);
        Task<TaskItem> EditTaskAsync(int id, UpdateTaskModel model, int userId);
        Task<TaskItem?> GetByIdAsync(int id, int userId);
        Task DeleteAsync(int id, int userId);
        Task<PagedResponse<TaskItem>> GetTasksAsync(int userId, int page = 1, int pageSize = 10, bool includeExpired = false, bool onlyExpired = false, int? categoryId = null, int? statusId = null);
    }
}