using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;

namespace TaskManager.Data
{
    public class TaskDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<CategoryItem> Categories { get; set; }

        public DbSet<StatusItem> Statuses { get; set; }

        public DbSet<PriorityItem> Priorities { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryItem>().HasData(
               new CategoryItem { Id = 1, Name = "Home", Icon = "🏠", CreatedDate = new DateTime(2025, 12, 2) },
               new CategoryItem { Id = 2, Name = "Work", Icon = "💼", CreatedDate = new DateTime(2025, 12, 2) },
               new CategoryItem { Id = 3, Name = "Study", Icon = "🎓", CreatedDate = new DateTime(2025, 12, 2) });

            modelBuilder.Entity<CategoryItem>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Name = "My 1st task", Description = "1st task description", CategoryId = 1, CreatedDate = new DateTime(2025, 12, 2), DueDate = new DateTime(2026, 12, 2), StatusId = 1, PriorityId = 2 },
                new TaskItem { Id = 2, Name = "My 2nd task", Description = "2nd task description", CategoryId = 1, CreatedDate = new DateTime(2025, 11, 2), DueDate = new DateTime(2025, 12, 2), StatusId = 1, PriorityId = 2 },
                new TaskItem { Id = 4, Name = "My 4th task", Description = "4th task description", CategoryId = 2, CreatedDate = new DateTime(2025, 11, 2), DueDate = new DateTime(2025, 12, 2), StatusId = 1, PriorityId = 2 },
                new TaskItem { Id = 3, Name = "My 3rd task", Description = "3rd task description", CategoryId = 2, CreatedDate = new DateTime(2025, 12, 2), DueDate = new DateTime(2026, 12, 2), StatusId = 1, PriorityId = 2 },
                new TaskItem { Id = 5, Name = "My 5th task", Description = "5th task description", CategoryId = 3, CreatedDate = new DateTime(2025, 12, 2), DueDate = new DateTime(2026, 12, 2), StatusId = 1, PriorityId = 2 },
                new TaskItem { Id = 6, Name = "My 6th task", Description = "6th task description", CategoryId = 3, CreatedDate = new DateTime(2025, 11, 2), DueDate = new DateTime(2025, 12, 2), StatusId = 1, PriorityId = 2 });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.Property(e => e.StatusId).HasDefaultValue(1);
            });

            modelBuilder.Entity<StatusItem>().HasData(
                new StatusItem { Id = 1, Name = "Новая", Order = 1, CreatedDate = new DateTime(2025, 12, 2) },
                new StatusItem { Id = 2, Name = "В работе", Order = 2, CreatedDate = new DateTime(2025, 12, 2) },
                new StatusItem { Id = 3, Name = "Завершена", Order = 3, CreatedDate = new DateTime(2025, 12, 2) },
                new StatusItem { Id = 4, Name = "Отменена", Order = 4, CreatedDate = new DateTime(2025, 12, 2) });

            modelBuilder.Entity<StatusItem>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<PriorityItem>().HasData(
                new PriorityItem { Id = 1, Name = "Низкий приоритет", Order = 1, Color = "#10B981" },
                new PriorityItem { Id = 2, Name = "Средний приоритет", Order = 50, Color = "#F59E0B" },
                new PriorityItem { Id = 3, Name = "Высокий приоритет", Order = 100, Color = "#EF4444" }
                );
        }
    }
}
