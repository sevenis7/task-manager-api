using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _taskContext;
        private readonly ILogger<TaskService> _logger;

        public TaskService(TaskDbContext context, ILogger<TaskService> logger)
        {
            _taskContext = context;
            _logger = logger;
        }

        public async Task<TaskItem> AddAsync(CreateTaskModel model, int userId)
        {
            if (model.CategoryId.HasValue)
            {
                var categoryExists = await _taskContext.Categories
                    .AnyAsync(x => x.Id == model.CategoryId);

                if (!categoryExists)
                    throw new ArgumentException("Category not found");
            }

            var priorityExists = await _taskContext.Priorities
                .AnyAsync(p => p.Id == model.PriorityId);

            if (!priorityExists)
                throw new ArgumentException("Priority not found");

            TaskItem task = MapCreateModelToEntity(model);

            task.UserId = userId;

            _taskContext.Tasks.Add(task);
            await _taskContext.SaveChangesAsync();

            _logger.LogInformation("User {userId} created task {taskId}", userId, task.Id);

            return await _taskContext.Tasks
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .FirstAsync(t => t.Id == task.Id);
        }

        public async Task<TaskItem?> GetByIdAsync(int id, int userId)
        {
            var task = await _taskContext.Tasks
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (task is null)
                throw new ArgumentException("Task not found or access denied");

            return task;
        }

        public async Task<TaskItem> EditTaskAsync(int id, UpdateTaskModel model, int userId)
        {
            var task = await GetByIdAsync(id, userId);

            task!.Name = model.Name;
            task.Description = model.Description;
            task.DueDate = model.DueDate;

            if (model.CategoryId.HasValue)
            {
                var categoryExists = await _taskContext.Categories
                    .AnyAsync(x => x.Id == model.CategoryId);

                if (!categoryExists)
                    throw new ArgumentException("Category not found");

                task.CategoryId = model.CategoryId.Value;
            }

            var priorityExists = await _taskContext.Priorities
                .AnyAsync(p => p.Id == model.PriorityId);

            if (!priorityExists)
                throw new ArgumentException("Priority not found");

            await _taskContext.SaveChangesAsync();

            _logger.LogInformation("User {userId} edited task {taskId}", userId, task.Id);

            return task;
        }

        public async Task<TaskItem> ChangeStatusAsync(int taskId, int statusId, int userId)
        {
            var task = await GetByIdAsync(taskId, userId);

            var statusExists = await _taskContext.Statuses.AnyAsync(x => x.Id == statusId);
            if (!statusExists)
                throw new ArgumentException("Status not found");

            task!.StatusId = statusId;

            await _taskContext.SaveChangesAsync();

            _logger.LogInformation("User {userId} changed task {taskId} status to {statusId}", userId, taskId, statusId);

            return task;
        }

        public async Task<List<TaskItem>> GetTasksAsync(
            int userId,
            bool includeExpired = false,
            bool onlyExpired = false,
            int? categoryId = null,
            int? statusId = null)
        {
            var query = _taskContext.Tasks
                .Include(x => x.Category)
                .Include(x => x.Status)
                .Include(x => x.Priority)
                .Where(x => x.UserId == userId)
                .AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId);

            if (statusId.HasValue)
                query = query.Where(x => x.StatusId == statusId);

            query = (includeExpired, onlyExpired) switch
            {
                (false, false) => query.Where(x => x.DueDate == null || x.DueDate >= DateTime.UtcNow),
                (true, false) => query,
                (_, true) => query.Where(x => x.DueDate != null && x.DueDate < DateTime.UtcNow)
            };

            return await query.ToListAsync();
        }

        public async Task DeleteAsync(int id, int userId)
        {
            var task = await _taskContext.Tasks.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (task is null)
                throw new ArgumentException("Task not found or access denied");

            _logger.LogInformation("User {userId} deleted task {taskId}", userId, task.Id);

            _taskContext.Tasks.Remove(task);
            await _taskContext.SaveChangesAsync();
        }

        private TaskItem MapCreateModelToEntity(CreateTaskModel source)
            => new()
            {
                Name = source.Name,
                Description = source.Description,
                DueDate = source.DueDate,
                CategoryId = source.CategoryId,
                PriorityId = source.PriorityId
            };

    }
}
