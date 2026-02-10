using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    public class StatusController : BaseController<StatusItemModel, StatusDto>
    {
        public StatusController(IBaseApiService<StatusItemModel, StatusDto> apiService) : base(apiService)
        {
        }
    }
}
