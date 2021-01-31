using Barbuuuda.Models.Logger;
using Barbuuuda.Models.MainPage;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<LoggerDto> Logs { get; set; }   // Таблица логов.

        public DbSet<FonDto> Fons { get; set; }     // Таблица фона.

        public DbSet<WorkDto> Works { get; set; }   // Таблица как это работает.

        public DbSet<WhyDto> Whies { get; set; }    // Таблица почему барбуда.

        public DbSet<PrivilegeDto> Privileges { get; set; }    // Таблица ЧТО ВЫ ПОЛУЧАЕТЕ.

        public DbSet<AdvantageDto> Advantages { get; set; }    // Таблица преимуществ.

        public DbSet<TaskTypeDto> TaskTypes { get; set; }  // Таблица типов заданий.

        public DbSet<TaskStatusDto> TaskStatuses { get; set; }    // Таблица статусов заданий.

        public DbSet<TaskCategoryDto> TaskCategories { get; set; }     // Таблица категорий заданий.

        public DbSet<HeaderTypeDto> Headers { get; set; }   // Таблица полей хидера.

        public DbSet<HopeDto> Hopes { get; set; }  // Таблица НАДЕЕМСЯ НА ДОЛГОЕ СОТРУДНИЧЕСТВО.

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
