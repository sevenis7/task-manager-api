using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;

namespace TaskManager.Data
{
    public class AuthDbContext : DbContext
    {
        public DbSet<UserItem> Users { get; set; }

        public DbSet<RoleItem> Roles { get; set; }

        public DbSet<RefreshTokenItem> RefreshTokens { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) 
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleItem>().HasData(
                new RoleItem { Id = 1, Name = "Admin" },
                new RoleItem { Id = 2, Name = "User" }
                );

        }
    }
}
