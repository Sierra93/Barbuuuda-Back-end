using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barbuuuda.Core.Data
{
    public sealed class IdentityDbContext : IdentityDbContext<UserEntity>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    }
}
