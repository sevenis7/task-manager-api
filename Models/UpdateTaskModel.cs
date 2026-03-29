using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class UpdateTaskModel
    {

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }

        public int? PriorityId { get; set; }
    }
}
