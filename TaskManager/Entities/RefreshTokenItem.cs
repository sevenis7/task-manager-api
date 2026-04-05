namespace TaskManager.Entities
{
    public class RefreshTokenItem
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public int UserId { get; set; }

        public UserItem? User {  get; set; } 
    }
}
