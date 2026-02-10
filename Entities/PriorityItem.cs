namespace TaskManager.Entities
{
    public class PriorityItem
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Color { get; set; }

        public int Order { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
