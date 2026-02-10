namespace TaskManager.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }

        public CategoryItem? Category { get; set; }

        public int StatusId { get; set; } = 1;

        public StatusItem Status { get; set; } = null!;

        public required int PriorityId { get; set; }

        public PriorityItem Priority { get; set; } = null!;
    }
}
