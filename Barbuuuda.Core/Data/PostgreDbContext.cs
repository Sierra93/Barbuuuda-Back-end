using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Barbuuuda.Core.Data
{
    /// <summary>
    /// Класс контекста БД PostgreSQL.
    /// </summary>
    public class PostgreDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }    // Таблица информации аккаунтов пользователей.

        public DbSet<TaskEntity> Tasks { get; set; }    // Таблица предложений поставщикам.

        public DbSet<TaskStatusEntity> TaskStatuses { get; set; }   // Таблица статусов заданий.

        public DbSet<TaskCategoryEntity> TaskCategories { get; set; }   // Таблица категорий заданий.

        public DbSet<TaskTypeEntity> TaskTypes { get; set; }   // Таблица типов заданий.

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options) { }

        public DbSet<QuestionEntity> Questions { get; set; }    // Таблица вопросов для теста исполнителей.

        public DbSet<AnswerVariantEntity> AnswerVariants { get; set; }    // Таблица ответов к тестам исполнителей.

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
