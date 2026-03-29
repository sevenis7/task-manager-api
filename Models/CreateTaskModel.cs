namespace TaskManager.Models
{
    public class CreateTaskModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }

        public int? PriorityId { get; set; }
    }
}
