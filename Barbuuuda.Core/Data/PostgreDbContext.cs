using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Entities.Knowlege;
using Barbuuuda.Models.Entities.Respond;
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
        /// <summary>
        /// Таблица пользователей.
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Таблица предложений поставщикам.
        /// </summary>
        public DbSet<TaskEntity> Tasks { get; set; }

        /// <summary>
        /// Таблица статусов заданий.
        /// </summary>
        public DbSet<TaskStatusEntity> TaskStatuses { get; set; }

        /// <summary>
        /// Таблица категорий заданий
        /// </summary>
        public DbSet<TaskCategoryEntity> TaskCategories { get; set; }

        /// <summary>
        ///  Таблица типов заданий.
        /// </summary>
        public DbSet<TaskTypeEntity> TaskTypes { get; set; }  

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options) { }

        /// <summary>
        /// Таблица вопросов для теста исполнителей.
        /// </summary>
        public DbSet<QuestionEntity> Questions { get; set; }

        /// <summary>
        /// Таблица ответов к тестам исполнителей.
        /// </summary>
        public DbSet<AnswerVariantEntity> AnswerVariants { get; set; }    

        /// <summary>
        /// Таблица шаблонов к ставкам заданий.
        /// </summary>
        public DbSet<RespondTemplateEntity> RespondTemplates { get; set; }

        /// <summary>
        /// Таблица категорий в БЗ.
        /// </summary>
        public DbSet<KnowlegeCategoryEntity> KnowlegeCategories { get; set; }

        /// <summary>
        /// Таблица статей БЗ.
        /// </summary>
        public DbSet<KnowlegeArticleEntity> KnowlegeArticles { get; set; }  

        /// <summary>
        /// Таблица ставок к заданиям.
        /// </summary>
        public DbSet<RespondEntity> Responds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        /// <summary>
        /// Таблица популярных статей.
        /// </summary>
        public DbSet<PopularArticleEntity> PopularArticles { get; set; }
    }
}
