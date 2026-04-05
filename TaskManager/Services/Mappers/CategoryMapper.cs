using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.Mappers
{
    public class CategoryMapper : IEntityMapper<CategoryItem, CategoryDto>
    {
        public CategoryDto MapToDto(CategoryItem entity)
            => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Icon = entity.Icon
            };
    }
}
