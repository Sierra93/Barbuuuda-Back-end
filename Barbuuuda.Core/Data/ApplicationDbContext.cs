﻿using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Logger;
using Barbuuuda.Models.MainPage;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Barbuuuda.Core.Data
{
    /// <summary>
    /// Класс представляет контекст для работы с MSSQL.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public DbSet<LoggerEntity> Logs { get; set; }   // Таблица логов.

        public DbSet<FonEntity> Fons { get; set; }     // Таблица фона.

        public DbSet<WorkEntity> Works { get; set; }   // Таблица как это работает.

        public DbSet<WhyEntity> Whies { get; set; }    // Таблица почему барбуда.

        public DbSet<PrivilegeEntity> Privileges { get; set; }    // Таблица ЧТО ВЫ ПОЛУЧАЕТЕ.

        public DbSet<AdvantageEntity> Advantages { get; set; }    // Таблица преимуществ.

        public DbSet<TaskTypeEntity> TaskTypes { get; set; }  // Таблица типов заданий.

        public DbSet<TaskStatusEntity> TaskStatuses { get; set; }    // Таблица статусов заданий.

        public DbSet<TaskCategoryEntity> TaskCategories { get; set; }     // Таблица категорий заданий.

        public DbSet<HeaderTypeEntity> Headers { get; set; }   // Таблица полей хидера.

        public DbSet<HopeEntity> Hopes { get; set; }  // Таблица НАДЕЕМСЯ НА ДОЛГОЕ СОТРУДНИЧЕСТВО.

        public DbSet<QuestionEntity> Questions { get; set; }    // Таблица вопросов для теста исполнителей.

        public DbSet<AnswerVariant> AnswerVariants { get; set; }    // Таблица ответов к тестам исполнителей.

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
