using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.APIService
{
    public class StatusApiService : BaseApiService<StatusItem, StatusItemModel, StatusDto>
    {
        public StatusApiService(
            IBaseService<StatusItem, StatusItemModel> service,
            IEntityMapper<StatusItem, StatusDto> mapper) 
            : base(service, mapper)
        {
        }
    }
}
