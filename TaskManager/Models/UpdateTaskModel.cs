using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class UpdateTaskModel
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }

        public int? PriorityId { get; set; }
    }
}
