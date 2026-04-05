using TaskManager.Models;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Контроллер управления приоритетами задач
    /// </summary>
    public class PriorityController : BaseController<PriorityItemModel, PriorityDto>
    {
        public PriorityController(IBaseApiService<PriorityItemModel, PriorityDto> apiService) : base(apiService)
        {
        }
    }
}
