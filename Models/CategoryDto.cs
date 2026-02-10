namespace TaskManager.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Icon { get; set; }
    }
}
