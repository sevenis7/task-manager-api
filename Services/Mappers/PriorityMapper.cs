using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.Mappers
{
    public class PriorityMapper : IEntityMapper<PriorityItem, PriorityDto>
    {
        public PriorityDto MapToDto(PriorityItem entity)
            => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Color = entity.Color,
                Order = entity.Order
            };
    }
}
