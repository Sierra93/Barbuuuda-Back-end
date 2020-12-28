using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Core.Data {
    public class PostgreDbContext : DbContext {
        public DbSet<UserDto> Users { get; set; }    // Таблица пользователей.

        public DbSet<TaskDto> Tasks { get; set; }    // Таблица предложений поставщикам.

        public DbSet<TaskSpecializationDto> TaskSpecializations { get; set; }   // Таблица специализаций.

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
