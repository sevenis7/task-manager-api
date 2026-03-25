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
        Task<List<TaskItem>> GetTasksAsync(int userId, bool includeExpired = false, bool onlyExpired = false, int? categoryId = null, int? statusId = null);
        Task DeleteAsync(int id, int userId);
    }
}