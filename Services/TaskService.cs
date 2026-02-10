using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _context;

        public TaskService(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> AddAsync(CreateTaskModel model)
        {
            if (model.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories
                    .AnyAsync(x => x.Id == model.CategoryId);

                if (!categoryExists)
                    throw new ArgumentException("Category not found");
            }

            var priorityExists = await _context.Priorities
                .AnyAsync(p => p.Id == model.PriorityId);

            if (!priorityExists)
                throw new ArgumentException("Priority not found");

            TaskItem task = MapCreateModelToEntity(model);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return await _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .FirstAsync(t => t.Id == task.Id);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            var task = await _context.Tasks
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
                var categoryExists = await _context.Categories
                    .AnyAsync(x => x.Id == model.CategoryId);

                if (!categoryExists)
                    throw new ArgumentException("Category not found");

                task.CategoryId = model.CategoryId.Value;
            }

            var priorityExists = await _context.Priorities
                .AnyAsync(p => p.Id == model.PriroityId);

            if (!priorityExists)
                throw new ArgumentException("Priority not found");

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> ChangeStatusAsync(int taskId, int statusId)
        {
            var task = await GetByIdAsync(taskId);
            if (task == null)
                throw new ArgumentException("Task not found");

            var statusExists = await _context.Statuses.AnyAsync(x => x.Id == statusId);
            if (!statusExists)
                throw new ArgumentException("Status not found");

            task.StatusId = statusId;

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskItem>> GetTasksAsync(
            bool includeExpired = false,
            bool onlyExpired = false,
            int? categoryId = null,
            int? statusId = null)
        {
            var query = _context.Tasks
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
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                throw new ArgumentException("Task not found");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
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
