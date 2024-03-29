﻿using Barbuuuda.Models.Entities.Chat;
using Barbuuuda.Models.Entities.Customer;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Entities.Knowlege;
using Barbuuuda.Models.Entities.Payment;
using Barbuuuda.Models.Entities.Respond;
using Barbuuuda.Models.Entities.Task;
using Barbuuuda.Models.Entities.User;
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
        private readonly DbContextOptions<PostgreDbContext> _options;

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

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

        /// <summary>
        /// Таблица популярных статей.
        /// </summary>
        public DbSet<PopularArticleEntity> PopularArticles { get; set; }

        /// <summary>
        /// Таблица статистики исполнителей.
        /// </summary>
        public DbSet<StatisticEntity> Statistics { get; set; }

        /// <summary>
        /// Таблица состояний пользователей.
        /// </summary>
        public DbSet<StateEntity> States { get; set; }

        /// <summary>
        /// Таблица информации о диалогах dbo.MainInfoDialogs.
        /// </summary>
        public DbSet<MainInfoDialogEntity> MainInfoDialogs { get; set; }

        /// <summary>
        /// Таблица участников диалога dbo.DialogMembers.
        /// </summary>
        public DbSet<DialogMemberEntity> DialogMembers { get; set; }

        /// <summary>
        /// Таблица сообщений dbo.DialogMessages.
        /// </summary>
        public DbSet<DialogMessageEntity> DialogMessages { get; set; }

        /// <summary>
        /// Таблица счетов пользователей dbo.Invoices.
        /// </summary>
        public DbSet<InvoiceEntity> Invoices { get; set; }

        /// <summary>
        /// Таблица отмененных приглашений.
        /// </summary>
        public DbSet<InviteEntity> Invities { get; set; }

        /// <summary>
        /// Таблица заказов.
        /// </summary>
        public DbSet<OrderEntity> Orders { get; set; }

        /// <summary>
        /// Таблица переходов.
        /// </summary>
        public DbSet<TransitionEntity> Transitions { get; set; }

        /// <summary>
        /// Таблица контрола селекта сортировки заданий.
        /// </summary>
        public DbSet<ControlSortEntity> ControlSorts { get; set; }

        /// <summary>
        /// Таблица контрола селекта фильтрации заданий.
        /// </summary> 
        public DbSet<ControlFilterEntity> ControlFilters { get; set; }

        /// <summary>
        /// Таблица просроченных заданий.
        /// </summary>
        public DbSet<OverdueTaskEntity> OverdueTasks { get; set; }
    }
}
