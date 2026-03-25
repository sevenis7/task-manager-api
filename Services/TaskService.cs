using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _taskContext;
        private readonly AuthDbContext _authContext;

        public TaskService(TaskDbContext context, AuthDbContext authContext)
        {
            _taskContext = context;
            _authContext = authContext;
        }

        public async Task<TaskItem> AddAsync(CreateTaskModel model, int userId)
        {
            var userExisted = await _authContext.Users.FindAsync(userId);

            if (userExisted is null)
                throw new ArgumentException("User not found");

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

            task.UserId = userExisted.Id;

            _taskContext.Tasks.Add(task);
            await _taskContext.SaveChangesAsync();

            return await _taskContext.Tasks
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .FirstAsync(t => t.Id == task.Id);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            var task = await _taskContext.Tasks
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(x => x.Id == id);

            return task;
        }

        public async Task<TaskItem> EditTaskAsync(int id, UpdateTaskModel model)
        {
            var task = await GetByIdAsync(id);

            if (task == null)
                throw new ArgumentException("Task not found");

            task.Name = model.Name;
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
                .AnyAsync(p => p.Id == model.PriroityId);

            if (!priorityExists)
                throw new ArgumentException("Priority not found");

            await _taskContext.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> ChangeStatusAsync(int taskId, int statusId)
        {
            var task = await GetByIdAsync(taskId);
            if (task == null)
                throw new ArgumentException("Task not found");

            var statusExists = await _taskContext.Statuses.AnyAsync(x => x.Id == statusId);
            if (!statusExists)
                throw new ArgumentException("Status not found");

            task.StatusId = statusId;

            await _taskContext.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskItem>> GetTasksAsync(
            bool includeExpired = false,
            bool onlyExpired = false,
            int? categoryId = null,
            int? statusId = null)
        {
            var query = _taskContext.Tasks
                .Include(x => x.Category)
                .Include(x => x.Status)
                .Include(x => x.Priority)
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

        public async Task DeleteAsync(int id)
        {
            var task = await _taskContext.Tasks.FindAsync(id);

            if (task == null)
                throw new ArgumentException("Task not found");

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
