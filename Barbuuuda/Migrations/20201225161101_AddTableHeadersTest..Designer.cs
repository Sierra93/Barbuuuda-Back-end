﻿// <auto-generated />
using System;
using Barbuuuda.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Barbuuuda.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201225161101_AddTableHeadersTest.")]
    partial class AddTableHeadersTest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("category_name");

                    b.HasKey("CategoryId");

                    b.ToTable("TaskCategories", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskDto", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("task_id")
                        .UseIdentityColumn();

                    b.Property<int>("CountOffers")
                        .HasColumnType("int")
                        .HasColumnName("count_offers");

                    b.Property<int>("CountViews")
                        .HasColumnType("int")
                        .HasColumnName("count_views");

                    b.Property<DateTime>("DateCreateTask")
                        .HasColumnType("datetime")
                        .HasColumnName("date_create_task");

                    b.Property<int?>("ExecutorId")
                        .HasColumnType("int")
                        .HasColumnName("executor_id");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int")
                        .HasColumnName("owner_id");

                    b.Property<int>("TaskCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("task_category_id");

                    b.Property<string>("TaskDetail")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("task_detail");

                    b.Property<decimal?>("TaskPrice")
                        .HasColumnType("money")
                        .HasColumnName("task_price");

                    b.Property<int>("TaskStatusId")
                        .HasColumnType("int")
                        .HasColumnName("task_status_id");

                    b.Property<string>("TaskTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("task_title");

                    b.Property<int>("TaskTypeId")
                        .HasColumnType("int")
                        .HasColumnName("task_type_id");

                    b.HasKey("TaskId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TaskCategoryId");

                    b.HasIndex("TaskStatusId");

                    b.HasIndex("TaskTypeId");

                    b.ToTable("Tasks", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskStatusDto", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("status_id")
                        .UseIdentityColumn();

                    b.Property<string>("StatusName")
                        .HasColumnType("nvarchar(100)")
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

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(200)")
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

                    b.Property<string>("ProfileField")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("profile_field");

                    b.HasKey("Id");

                    b.ToTable("Headers", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.User.UserDto", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id")
                        .UseIdentityColumn();

                    b.Property<int>("CountNegative")
                        .HasColumnType("int")
                        .HasColumnName("count_negative");

                    b.Property<int>("CountPositive")
                        .HasColumnType("int")
                        .HasColumnName("count_positive");

                    b.Property<string>("DateRegister")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("date_register");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("IsOnline")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("is_online");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_name");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("patronymic");

                    b.Property<double>("Rating")
                        .HasColumnType("float")
                        .HasColumnName("rating");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("user_email");

                    b.Property<string>("UserIcon")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("user_icon");

                    b.Property<string>("UserLogin")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("user_login");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("user_password");

                    b.Property<string>("UserPhone")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("user_phone");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_type");

                    b.HasKey("UserId");

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskDto", b =>
                {
                    b.HasOne("Barbuuuda.Models.User.UserDto", "Executor")
                        .WithMany()
                        .HasForeignKey("ExecutorId");

                    b.HasOne("Barbuuuda.Models.User.UserDto", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbuuuda.Models.Task.TaskCategoryDto", "Category")
                        .WithMany()
                        .HasForeignKey("TaskCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbuuuda.Models.Task.TaskStatusDto", "Status")
                        .WithMany()
                        .HasForeignKey("TaskStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbuuuda.Models.Task.TaskTypeDto", "Type")
                        .WithMany()
                        .HasForeignKey("TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Executor");

                    b.Navigation("Owner");

                    b.Navigation("Status");

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
