namespace TaskManager.Entities
{
    public class UserItem
    {
        public int Id { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required int RoleId { get; set; }

        public RoleItem? Role { get; set; }

        public ICollection<RefreshTokenItem> RefreshTokens { get; set; } = new List<RefreshTokenItem>();
    }
}
