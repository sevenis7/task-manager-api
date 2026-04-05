namespace TaskManager.Entities
{
    public class CategoryItem
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Icon { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
