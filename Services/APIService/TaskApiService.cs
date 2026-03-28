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
            var task = await _taskService.AddAsync(model, userId);

            return _taskMapper.MapToDto(task);
        }

        public async Task<TaskDto> EditTaskAsync(int id, UpdateTaskModel model, int userId)
        {
            var task = await _taskService.EditTaskAsync(id, model, userId);

            return _taskMapper.MapToDto(task);

        }

        public async Task<TaskDto> ChangeStatusAsync(int id, int statusId, int userId)
        {
            var task = await _taskService.ChangeStatusAsync(id, statusId, userId);

            return _taskMapper.MapToDto(task);
        }

        public async Task<PagedResponse<TaskDto>> GetTasksAsync(
            int userId,
            int page = 1,
            int pageSize = 10,
            bool includeExpired = false,
            bool onlyExpired = false,
            int? categoryId = null,
            int? statusId = null)
        {
            var pagedTasks = await _taskService.GetTasksAsync(
                userId,
                page,
                pageSize,
                includeExpired,
                onlyExpired,
                categoryId,
                statusId);

            var dtos = _taskMapper.MapCollectionToDto(pagedTasks.Items);

            return new PagedResponse<TaskDto>(
                dtos,
                pagedTasks.TotalCount,
                pagedTasks.Page,
                pagedTasks.PageSize);
        }

        public async Task DeleteAsync(int id, int userId)
        {
            await _taskService.DeleteAsync(id, userId);
        }
    }
}
