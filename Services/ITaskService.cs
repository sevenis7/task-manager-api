using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<TaskItem> AddAsync(CreateTaskModel model);
        Task<TaskItem> ChangeStatusAsync(int taskId, int statusId);
        Task<TaskItem> EditTaskAsync(int id, UpdateTaskModel model);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<List<TaskItem>> GetTasksAsync(bool includeExpired = false, bool onlyExpired = false, int? categoryId = null, int? statusId = null);
        Task DeleteAsync(int id);
    }
}