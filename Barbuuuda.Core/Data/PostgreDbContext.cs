﻿using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Core.Data {
    public class PostgreDbContext : DbContext {
        public DbSet<UserDto> Users { get; set; }    // Таблица пользователей.

        public DbSet<TaskDto> Tasks { get; set; }    // Таблица предложений поставщикам.

        public DbSet<TaskStatusDto> TaskStatuses { get; set; }   // Таблица статусов заданий.

        public DbSet<TaskCategoryDto> TaskCategories { get; set; }   // Таблица категорий заданий.

        public DbSet<TaskTypeDto> TaskTypes { get; set; }   // Таблица типов заданий.

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}