using TaskManager.Entities;

namespace TaskManager.Models
{
    public class TaskDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }

        public int StatusId { get; set; }

        public int PriorityId { get; set; }

        public CategoryDto? Category { get; set; }

        public StatusDto Status { get; set; } = null!;

        public PriorityDto Priority { get; set; } = null!;

    }
}
