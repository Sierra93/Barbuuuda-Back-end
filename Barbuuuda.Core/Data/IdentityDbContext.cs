using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barbuuuda.Core.Data
{
    /// <summary>
    /// Класс представляет контекст для работы с PostgreSQL именно с таблицами Identity.
    /// </summary>
    public sealed class IdentityDbContext : IdentityDbContext<UserEntity>
    {
        public DbSet<UserEntity> AspNetUsers { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    }
}
