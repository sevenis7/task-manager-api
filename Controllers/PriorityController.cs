using TaskManager.Models;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    public class PriorityController : BaseController<PriorityItemModel, PriorityDto>
    {
        public PriorityController(IBaseApiService<PriorityItemModel, PriorityDto> apiService) : base(apiService)
        {
        }
    }
}
