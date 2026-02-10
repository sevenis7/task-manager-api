using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class StatusService : BaseService<StatusItem, StatusItemModel>
    {
        public StatusService(TaskDbContext context) : base(context)
        {
        }

        protected override StatusItem MapToEntity(StatusItemModel model)
            => new() { Name = model.Name, Order = model.Order };
    }
}
