namespace TaskManager.Entities
{
    public class UserItem
    {
        public int Id { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }


        public int RoleId { get; set; }

        public required RoleItem Role { get; set; }

        public ICollection<RefreshTokenItem> RefreshTokens { get; set; } = new List<RefreshTokenItem>();
    }
}
