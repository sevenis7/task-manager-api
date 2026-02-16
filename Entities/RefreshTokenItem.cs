namespace TaskManager.Entities
{
    public class RefreshTokenItem
    {
        public int Id { get; set; }

        public required string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public int UserId { get; set; }

        public UserItem User {  get; set; } 
    }
}
