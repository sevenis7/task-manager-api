using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class CategoryService : BaseService<CategoryItem, CategoryItemModel>
    {
        public CategoryService(TaskDbContext context) : base(context)
        {
        }

        protected override CategoryItem MapToEntity(CategoryItemModel model)
            => new() { Name = model.Name, Icon = model.Icon };
    }
}
