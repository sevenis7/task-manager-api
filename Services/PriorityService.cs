using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class PriorityService : BaseService<PriorityItem, PriorityItemModel>
    {
        public PriorityService(TaskDbContext context) :base(context)
        {
            
        }

        protected override PriorityItem MapToEntity(PriorityItemModel model)
            => new PriorityItem { Name = model.Name, Color = model.Color, Order = model.Order};
    }
}
