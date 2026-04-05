namespace TaskManager.Entities
{
    public class StatusItem
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int Order { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
