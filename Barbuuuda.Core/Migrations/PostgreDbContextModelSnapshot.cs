﻿// <auto-generated />
using System;
using Barbuuuda.Core.Data;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Barbuuuda.Core.Migrations
{
    [DbContext(typeof(PostgreDbContext))]
    partial class PostgreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Barbuuuda.Models.Entities.Chat.DialogMemberEntity", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("serial")
                        .HasColumnName("MemberId")
                        .UseIdentityByDefaultColumn();

                    b.Property<long>("DialogId")
                        .HasColumnType("serial");

                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("UserId");

                    b.Property<DateTime>("Joined")
                        .HasColumnType("timestamp")
                        .HasColumnName("Joined");

                    b.HasKey("MemberId");

                    b.HasIndex("DialogId");

                    b.HasIndex("Id");

                    b.ToTable("DialogMembers", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Chat.DialogMessageEntity", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("serial")
                        .HasColumnName("MessageId")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("Created");

                    b.Property<long>("DialogId")
                        .HasColumnType("serial");

                    b.Property<bool>("IsMyMessage")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text")
                        .HasColumnName("Message");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("MessageId");

                    b.HasIndex("DialogId");

                    b.HasIndex("UserId");

                    b.ToTable("DialogMessages", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Chat.MainInfoDialogEntity", b =>
                {
                    b.Property<long>("DialogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("serial")
                        .HasColumnName("DialogId")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("Created");

                    b.Property<string>("DialogName")
                        .HasColumnType("varchar(300)")
                        .HasColumnName("DialogName");

                    b.HasKey("DialogId");

                    b.ToTable("MainInfoDialogs", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Executor.AnswerVariantEntity", b =>
                {
                    b.Property<int>("AnswerVariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("AnswerVariantId")
                        .UseIdentityByDefaultColumn();

                    b.Property<AnswerVariant[]>("AnswerVariantText")
                        .HasColumnType("jsonb")
                        .HasColumnName("AnswerVariantText");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer")
                        .HasColumnName("QuestionId");

                    b.HasKey("AnswerVariantId");

                    b.ToTable("AnswerVariants", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Executor.QuestionEntity", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("QuestionId")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("NumberQuestion")
                        .HasColumnType("integer")
                        .HasColumnName("NumberQuestion");

                    b.Property<string>("QuestionText")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("QuestionText");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Executor.StatisticEntity", b =>
                {
                    b.Property<int>("StatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CategoryCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("CategoryCode");

                    b.Property<long>("CountNegative")
                        .HasColumnType("bigint")
                        .HasColumnName("CountNegative");

                    b.Property<long>("CountPositive")
                        .HasColumnType("bigint")
                        .HasColumnName("CountPositive");

                    b.Property<long>("CountTaskCategory")
                        .HasColumnType("bigint")
                        .HasColumnName("CountTaskCategory");

                    b.Property<long>("CountTotalCompletedTask")
                        .HasColumnType("bigint")
                        .HasColumnName("CountTotalCompletedTask");

                    b.Property<string>("ExecutorId")
                        .HasColumnType("text");

                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnName("Rating");

                    b.HasKey("StatId");

                    b.HasIndex("Id");

                    b.ToTable("ExecutorStatistic", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Knowlege.KnowlegeArticleEntity", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ArticleDetails")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("ArticleDetails");

                    b.Property<string>("ArticleTitle")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("ArticleTitle");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<bool>("HasImage")
                        .HasColumnType("bool")
                        .HasColumnName("HasImage");

                    b.Property<int>("HelpfulCount")
                        .HasColumnType("integer")
                        .HasColumnName("HelpfulCount");

                    b.Property<string>("ImageUrl")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("ArticleDetails");

                    b.Property<int>("NotHelpfulCount")
                        .HasColumnType("integer")
                        .HasColumnName("NotHelpfulCount");

                    b.HasKey("ArticleId");

                    b.HasIndex("CategoryId");

                    b.ToTable("KnowlegeArticles", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Knowlege.KnowlegeCategoryEntity", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("ArticlesCount")
                        .HasColumnType("int")
                        .HasColumnName("ArticlesCount");

                    b.Property<string>("CategoryMainTitle")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("CategoryMainTitle");

                    b.Property<string>("CategoryTitle")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("CategoryTitle");

                    b.Property<string>("CategoryTooltip")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CategoryTooltip");

                    b.HasKey("CategoryId");

                    b.ToTable("KnowlegeCategories", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Knowlege.PopularArticleEntity", b =>
                {
                    b.Property<int>("PopularId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ArticleTitle")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("ArticleTitle");

                    b.Property<int>("HelpfulCount")
                        .HasColumnType("integer")
                        .HasColumnName("HelpfulCount");

                    b.HasKey("PopularId");

                    b.ToTable("PopularArticles", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Payment.InvoiceEntity", b =>
                {
                    b.Property<long>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ScoreId")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Currency");

                    b.Property<decimal>("InvoiceAmount")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnName("InvoiceAmount");

                    b.Property<string>("ScoreEmail")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("ScoreEmail");

                    b.Property<int?>("ScoreNumber")
                        .HasColumnType("int4")
                        .HasColumnName("ScoreNumber");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("UserId");

                    b.HasKey("ScoreId");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Respond.RespondEntity", b =>
                {
                    b.Property<int>("RespondId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("RespondId")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("Comment");

                    b.Property<string>("ExecutorId")
                        .HasColumnType("text")
                        .HasColumnName("ExecutorId");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("Price");

                    b.Property<long?>("TaskId")
                        .HasColumnType("bigint")
                        .HasColumnName("TaskId");

                    b.HasKey("RespondId");

                    b.ToTable("Responds", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Respond.RespondTemplateEntity", b =>
                {
                    b.Property<int>("TemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ExecutorId")
                        .HasColumnType("text");

                    b.Property<Guid>("TemplateCode")
                        .HasColumnType("uuid");

                    b.Property<string>("TemplateComment")
                        .HasColumnType("text");

                    b.Property<string>("TemplateName")
                        .HasColumnType("text");

                    b.HasKey("TemplateId");

                    b.ToTable("RespondTemplates", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.User.StateEntity", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bool")
                        .HasColumnName("IsOnline");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("StateId");

                    b.HasIndex("Id");

                    b.ToTable("State", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskCategoryEntity", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("category_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CategoryCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("category_code");

                    b.Property<string>("CategoryName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("category_name");

                    b.Property<Specialization[]>("Specializations")
                        .HasColumnType("jsonb")
                        .HasColumnName("specializations");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("CategoryId");

                    b.ToTable("TaskCategories", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskEntity", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("TaskId")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CategoryCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("CategoryCode");

                    b.Property<int>("CountOffers")
                        .HasColumnType("integer")
                        .HasColumnName("CountOffers");

                    b.Property<int>("CountViews")
                        .HasColumnType("integer")
                        .HasColumnName("CountViews");

                    b.Property<string>("ExecutorId")
                        .HasColumnType("varchar(150)")
                        .HasColumnName("ExecutorId");

                    b.Property<bool>("IsPay")
                        .HasColumnType("boolean");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasColumnName("OwnerId");

                    b.Property<string>("SpecCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("SpecCode");

                    b.Property<string>("StatusCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("StatusCode");

                    b.Property<DateTime>("TaskBegda")
                        .HasColumnType("timestamp")
                        .HasColumnName("TaskBegda");

                    b.Property<string>("TaskDetail")
                        .HasColumnType("text")
                        .HasColumnName("TaskDetail");

                    b.Property<DateTime>("TaskEndda")
                        .HasColumnType("timestamp")
                        .HasColumnName("TaskEndda");

                    b.Property<decimal?>("TaskPrice")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnName("TaskPrice");

                    b.Property<string>("TaskTitle")
                        .HasColumnType("text")
                        .HasColumnName("TaskTitle");

                    b.Property<string>("TypeCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TypeCode");

                    b.HasKey("TaskId");

                    b.ToTable("Tasks", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskStatusEntity", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("status_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("StatusCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("status_code");

                    b.Property<string>("StatusName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("status_name");

                    b.HasKey("StatusId");

                    b.ToTable("TaskStatuses", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.Task.TaskTypeEntity", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("type_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("TypeCode")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("type_code");

                    b.Property<string>("TypeName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("type_name");

                    b.HasKey("TypeId");

                    b.ToTable("TaskTypes", "dbo");
                });

            modelBuilder.Entity("Barbuuuda.Models.User.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AboutInfo")
                        .HasColumnType("text")
                        .HasColumnName("AboutInfo");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("Age");

                    b.Property<string>("City")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("City");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateRegister")
                        .HasColumnType("timestamp")
                        .HasColumnName("DateRegister");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("FirstName");

                    b.Property<string>("Gender")
                        .HasColumnType("varchar(1)")
                        .HasColumnName("Gender");

                    b.Property<bool>("IsSuccessedTest")
                        .HasColumnType("bool")
                        .HasColumnName("IsSuccessedTest");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("LastName");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Patronymic");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Plan")
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Plan");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("boolean")
                        .HasColumnName("RememberMe");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<ExecutorSpecialization[]>("Specializations")
                        .HasColumnType("jsonb")
                        .HasColumnName("ExecutorSpecializations");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserIcon")
                        .HasColumnType("text")
                        .HasColumnName("UserIcon");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<string>("UserPassword")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("UserPassword");

                    b.Property<string>("UserRole")
                        .HasColumnType("varchar(1)")
                        .HasColumnName("UserRole");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Chat.DialogMemberEntity", b =>
                {
                    b.HasOne("Barbuuuda.Models.Entities.Chat.MainInfoDialogEntity", "Dialog")
                        .WithMany()
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbuuuda.Models.User.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("Id");

                    b.Navigation("Dialog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Chat.DialogMessageEntity", b =>
                {
                    b.HasOne("Barbuuuda.Models.Entities.Chat.MainInfoDialogEntity", "Dialog")
                        .WithMany()
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbuuuda.Models.User.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Dialog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Executor.StatisticEntity", b =>
                {
                    b.HasOne("Barbuuuda.Models.User.UserEntity", "Executor")
                        .WithMany()
                        .HasForeignKey("Id");

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Knowlege.KnowlegeArticleEntity", b =>
                {
                    b.HasOne("Barbuuuda.Models.Entities.Knowlege.KnowlegeCategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.Payment.InvoiceEntity", b =>
                {
                    b.HasOne("Barbuuuda.Models.User.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Barbuuuda.Models.Entities.User.StateEntity", b =>
                {
                    b.HasOne("Barbuuuda.Models.User.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("Id");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
