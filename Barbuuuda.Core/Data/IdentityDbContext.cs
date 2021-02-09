using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Barbuuuda.Core.Data
{
    public sealed class IdentityDbContext : IdentityDbContext<UserEntity>
    {
        public DbSet<UserEntity> AspNetUsers { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    }
}
