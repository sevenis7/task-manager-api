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

        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return null;

            return _taskMapper.MapToDto(task);
        }

        public async Task<TaskDto> AddAsync(CreateTaskModel model)
        {
            try
            {
                var task = await _taskService.AddAsync(model);

                return _taskMapper.MapToDto(task);
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }

        public async Task<TaskDto> EditTaskAsync(int id, UpdateTaskModel model)
        {
            try
            {
                var task = await _taskService.EditTaskAsync(id, model);

                return _taskMapper.MapToDto(task);
            }
            catch (ArgumentException ex)
            {
                throw;
            }

        }

        public async Task<TaskDto> ChangeStatusAsync(int id, int statusId)
        {
            try
            {
                var task = await _taskService.ChangeStatusAsync(id, statusId);

                return _taskMapper.MapToDto(task);
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }

        public async Task<List<TaskDto>> GetTasksAsync(
            bool includeExpired,
            bool onlyExpired,
            int? categoryId,
            int? statusId)
        {
            var tasks = await _taskService.GetTasksAsync(includeExpired, onlyExpired, categoryId, statusId);

            return _taskMapper.MapCollectionToDto(tasks);
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _taskService.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
    }
}
