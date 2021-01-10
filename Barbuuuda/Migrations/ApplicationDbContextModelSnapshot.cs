﻿// <auto-generated />
using Barbuuuda.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Barbuuuda.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Barbuuuda.Models.MainPage.AdvantageDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("MainTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("main_title");

                    b.Property<string>("SecondTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("second_title");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.ToTable("Advantages", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.MainPage.FonDto", b =>
                {
                    b.Property<int>("FonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("fon_id")
                        .UseIdentityColumn();

                    b.Property<string>("BtnText")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("btn-text");

                    b.Property<string>("MainTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("main_title");

                    b.Property<string>("SecondTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("second_title");

                    b.HasKey("FonId");

                    b.ToTable("Fons", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.MainPage.PrivilegeDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("text");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("Privileges", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.MainPage.WhyDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("MainTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("main_titie");

                    b.Property<string>("SecondTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("second_title");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.ToTable("Whies", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.MainPage.WorkDto", b =>
                {
                    b.Property<int>("WorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("work_id")
                        .UseIdentityColumn();

                    b.Property<string>("BlockText")
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("block_text");

                    b.Property<string>("BlockTitle")
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("block_title");

                    b.Property<string>("MainTitle")
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("main_title");

                    b.Property<string>("SecondTitle")
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("second_title");

                    b.HasKey("WorkId");

                    b.ToTable("Works", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskCategoryDto", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("category_code");

                    b.Property<string>("CategoryName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("category_name");

                    b.HasKey("CategoryId");

                    b.ToTable("TaskCategories", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskStatusDto", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("status_id")
                        .UseIdentityColumn();

                    b.Property<string>("StatusCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("status_code");

                    b.Property<string>("StatusName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("status_name");

                    b.HasKey("StatusId");

                    b.ToTable("TaskStatuses", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskTypeDto", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("type_id")
                        .UseIdentityColumn();

                    b.Property<string>("TypeCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("type_code");

                    b.Property<string>("TypeName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("type_name");

                    b.HasKey("TypeId");

                    b.ToTable("TaskTypes", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.User.HeaderTypeDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("HeaderField")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("header_field");

                    b.Property<string>("HeaderIcon")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("header_icon");

                    b.Property<string>("HeaderType")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("header_type");

                    b.Property<bool>("IsProfile")
                        .HasColumnType("bit")
                        .HasColumnName("is_profile");

                    b.Property<string>("ProfileField")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("profile_field");

                    b.HasKey("Id");

                    b.ToTable("Headers", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
