using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.APIService
{
    public class PriorityApiService : BaseApiService<PriorityItem, PriorityItemModel, PriorityDto>
    {
        public PriorityApiService(
            IBaseService<PriorityItem, PriorityItemModel> service,
            IEntityMapper<PriorityItem, PriorityDto> mapper) 
            : base(service, mapper)
        {
        }
    }
}
