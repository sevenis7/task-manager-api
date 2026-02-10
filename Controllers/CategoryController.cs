using TaskManager.Models;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    public class CategoryController : BaseController<CategoryItemModel, CategoryDto>
    {
        public CategoryController(IBaseApiService<CategoryItemModel, CategoryDto> apiService) : base(apiService)
        {
        }
    }
}
