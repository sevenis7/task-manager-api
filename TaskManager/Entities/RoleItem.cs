namespace TaskManager.Entities
{
    public class RoleItem
    {
        public int Id { get; set; }

        public required string Name { get; set;  }

        public ICollection<UserItem> Users { get; set; } = new List<UserItem>();
    }
}