using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Контроллер управления статусами задач
    /// </summary>
    public class StatusController : BaseController<StatusItemModel, StatusDto>
    {
        public StatusController(IBaseApiService<StatusItemModel, StatusDto> apiService) : base(apiService)
        {
        }
    }
}
