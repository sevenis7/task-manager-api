using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.Mappers
{
    public class StatusMapper : IEntityMapper<StatusItem, StatusDto>
    {
        public StatusDto MapToDto(StatusItem entity)
            => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Order = entity.Order
            };
    }
}
