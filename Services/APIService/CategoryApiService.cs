using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.APIService
{
    public class CategoryApiService : BaseApiService<CategoryItem, CategoryItemModel, CategoryDto>
    {
        public CategoryApiService(
            IBaseService<CategoryItem, CategoryItemModel> service, 
            IEntityMapper<CategoryItem, CategoryDto> mapper)
            : base(service, mapper)
        {
        }
    }
}
