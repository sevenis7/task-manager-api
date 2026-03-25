using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.APIService
{
    public class TaskApiService : ITaskApiService
    {
        private readonly ITaskService _taskService;
        private readonly IEntityMapper<TaskItem, TaskDto> _taskMapper;

        public TaskApiService(ITaskService taskService, IEntityMapper<TaskItem, TaskDto> taskMapper)
        {
            _taskService = taskService;
            _taskMapper = taskMapper;
        }

        public async Task<TaskDto> GetByIdAsync(int id, int userId)
        {
            var task = await _taskService.GetByIdAsync(id, userId);

            return _taskMapper.MapToDto(task!);
        }

        public async Task<TaskDto> AddAsync(CreateTaskModel model, int userId)
        {
            try
            {
                var task = await _taskService.AddAsync(model, userId);

                return _taskMapper.MapToDto(task);
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }

        public async Task<TaskDto> EditTaskAsync(int id, UpdateTaskModel model, int userId)
        {
            try
            {
                var task = await _taskService.EditTaskAsync(id, model, userId);

                return _taskMapper.MapToDto(task);
            }
            catch (ArgumentException ex)
            {
                throw;
            }

        }

        public async Task<TaskDto> ChangeStatusAsync(int id, int statusId, int userId)
        {
            try
            {
                var task = await _taskService.ChangeStatusAsync(id, statusId, userId);

                return _taskMapper.MapToDto(task);
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }

        public async Task<List<TaskDto>> GetTasksAsync(
            int userId,
            bool includeExpired,
            bool onlyExpired,
            int? categoryId,
            int? statusId)
        {
            var tasks = await _taskService.GetTasksAsync(userId, includeExpired, onlyExpired, categoryId, statusId);

            return _taskMapper.MapCollectionToDto(tasks);
        }

        public async Task DeleteAsync(int id, int userId)
        {
            try
            {
                await _taskService.DeleteAsync(id, userId);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
    }
}
