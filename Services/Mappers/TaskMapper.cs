using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.Mappers
{
    public class TaskMapper : IEntityMapper<TaskItem, TaskDto>
    {
        private readonly IEntityMapper<StatusItem, StatusDto> _statusMapper;
        private readonly IEntityMapper<CategoryItem, CategoryDto> _categoryMapper;
        private readonly IEntityMapper<PriorityItem, PriorityDto> _priorityMapper;

        public TaskMapper(
            IEntityMapper<StatusItem, StatusDto> statusMapper,
            IEntityMapper<CategoryItem, CategoryDto> categoryMapper,
            IEntityMapper<PriorityItem, PriorityDto> priorityMapper)
        {
            _statusMapper = statusMapper;
            _categoryMapper = categoryMapper;
            _priorityMapper = priorityMapper;
        }

        public TaskDto MapToDto(TaskItem entity)
        {
            if (entity.Status is null)
                throw new InvalidOperationException("Status not loaded.");

            if (entity.CategoryId.HasValue && entity.Category is null)
                throw new InvalidOperationException("Category not loaded but CategoryId exists");

            return new TaskDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DueDate = entity.DueDate,
                StatusId = entity.StatusId,
                CategoryId = entity.CategoryId,
                PriorityId = entity.PriorityId,
                Status = _statusMapper.MapToDto(entity.Status),
                Category = entity.Category != null ? _categoryMapper.MapToDto(entity.Category) : null,
                Priority = _priorityMapper.MapToDto(entity.Priority)
            };
        }
    }
}
