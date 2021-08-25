using System;
using Barbuuuda.Models.Entities.Executor;
using Barbuuuda.Models.Task;
using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Barbuuuda.Core.Migrations
{
    public partial class RemoveColumnScoreUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.EnsureSchema(
            //    name: "dbo");

            //migrationBuilder.CreateTable(
            //    name: "AnswerVariants",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        AnswerVariantId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        AnswerVariantText = table.Column<AnswerVariant[]>(type: "jsonb", nullable: true),
            //        QuestionId = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AnswerVariants", x => x.AnswerVariantId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "text", nullable: false),
            //        UserPassword = table.Column<string>(type: "varchar(100)", nullable: true),
            //        UserRole = table.Column<string>(type: "varchar(1)", nullable: true),
            //        LastName = table.Column<string>(type: "varchar(100)", nullable: true),
            //        FirstName = table.Column<string>(type: "varchar(100)", nullable: true),
            //        Patronymic = table.Column<string>(type: "varchar(100)", nullable: true),
            //        UserIcon = table.Column<string>(type: "text", nullable: true),
            //        DateRegister = table.Column<DateTime>(type: "timestamp", nullable: false),
            //        AboutInfo = table.Column<string>(type: "text", nullable: true),
            //        Plan = table.Column<string>(type: "varchar(10)", nullable: true),
            //        City = table.Column<string>(type: "varchar(200)", nullable: true),
            //        Age = table.Column<int>(type: "integer", nullable: false),
            //        Gender = table.Column<string>(type: "varchar(1)", nullable: true),
            //        RememberMe = table.Column<bool>(type: "boolean", nullable: false),
            //        ExecutorSpecializations = table.Column<ExecutorSpecialization[]>(type: "jsonb", nullable: true),
            //        IsSuccessedTest = table.Column<bool>(type: "bool", nullable: false),
            //        UserName = table.Column<string>(type: "text", nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "text", nullable: true),
            //        Email = table.Column<string>(type: "text", nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "text", nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
            //        PasswordHash = table.Column<string>(type: "text", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "text", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "text", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "KnowlegeCategories",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        CategoryId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        CategoryTitle = table.Column<string>(type: "varchar(200)", nullable: true),
            //        CategoryTooltip = table.Column<string>(type: "varchar(400)", nullable: true),
            //        ArticlesCount = table.Column<int>(type: "int", nullable: false),
            //        CategoryMainTitle = table.Column<string>(type: "varchar(200)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_KnowlegeCategories", x => x.CategoryId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MainInfoDialogs",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        DialogId = table.Column<long>(type: "serial", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        DialogName = table.Column<string>(type: "varchar(300)", nullable: true),
            //        Created = table.Column<DateTime>(type: "timestamp", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MainInfoDialogs", x => x.DialogId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PopularArticles",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        PopularId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        ArticleTitle = table.Column<string>(type: "varchar(400)", nullable: true),
            //        HelpfulCount = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PopularArticles", x => x.PopularId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Questions",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        QuestionId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NumberQuestion = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Questions", x => x.QuestionId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Responds",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        RespondId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Price = table.Column<decimal>(type: "numeric", nullable: true),
            //        TaskId = table.Column<long>(type: "bigint", nullable: true),
            //        Comment = table.Column<string>(type: "text", nullable: true),
            //        ExecutorId = table.Column<string>(type: "text", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Responds", x => x.RespondId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RespondTemplates",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        TemplateId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        TemplateCode = table.Column<Guid>(type: "uuid", nullable: false),
            //        ExecutorId = table.Column<string>(type: "text", nullable: true),
            //        TemplateName = table.Column<string>(type: "text", nullable: true),
            //        TemplateComment = table.Column<string>(type: "text", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RespondTemplates", x => x.TemplateId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TaskCategories",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        category_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        category_code = table.Column<string>(type: "varchar(100)", nullable: true),
            //        category_name = table.Column<string>(type: "varchar(100)", nullable: true),
            //        specializations = table.Column<Specialization[]>(type: "jsonb", nullable: true),
            //        url = table.Column<string>(type: "text", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TaskCategories", x => x.category_id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tasks",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        TaskId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        OwnerId = table.Column<string>(type: "varchar(150)", nullable: false),
            //        ExecutorId = table.Column<string>(type: "varchar(150)", nullable: true),
            //        TaskBegda = table.Column<DateTime>(type: "timestamp", nullable: false),
            //        TaskEndda = table.Column<DateTime>(type: "timestamp", nullable: false),
            //        CountOffers = table.Column<int>(type: "integer", nullable: false),
            //        CountViews = table.Column<int>(type: "integer", nullable: false),
            //        TypeCode = table.Column<string>(type: "varchar(100)", nullable: true),
            //        StatusCode = table.Column<string>(type: "varchar(100)", nullable: true),
            //        CategoryCode = table.Column<string>(type: "varchar(100)", nullable: true),
            //        TaskPrice = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
            //        TaskTitle = table.Column<string>(type: "text", nullable: true),
            //        TaskDetail = table.Column<string>(type: "text", nullable: true),
            //        SpecCode = table.Column<string>(type: "varchar(100)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tasks", x => x.TaskId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TaskStatuses",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        status_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        status_code = table.Column<string>(type: "varchar(100)", nullable: true),
            //        status_name = table.Column<string>(type: "varchar(100)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TaskStatuses", x => x.status_id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TaskTypes",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        type_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        type_code = table.Column<string>(type: "varchar(100)", nullable: true),
            //        type_name = table.Column<string>(type: "varchar(100)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TaskTypes", x => x.type_id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExecutorStatistic",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        StatId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        ExecutorId = table.Column<string>(type: "text", nullable: true),
            //        Id = table.Column<string>(type: "text", nullable: true),
            //        CountTotalCompletedTask = table.Column<long>(type: "bigint", nullable: false),
            //        CountPositive = table.Column<long>(type: "bigint", nullable: false),
            //        CountNegative = table.Column<long>(type: "bigint", nullable: false),
            //        Rating = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
            //        Score = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
            //        CategoryCode = table.Column<string>(type: "varchar(100)", nullable: true),
            //        CountTaskCategory = table.Column<long>(type: "bigint", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExecutorStatistic", x => x.StatId);
            //        table.ForeignKey(
            //            name: "FK_ExecutorStatistic_AspNetUsers_Id",
            //            column: x => x.Id,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "State",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        StateId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        UserId = table.Column<string>(type: "text", nullable: true),
            //        Id = table.Column<string>(type: "text", nullable: true),
            //        IsOnline = table.Column<bool>(type: "bool", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_State", x => x.StateId);
            //        table.ForeignKey(
            //            name: "FK_State_AspNetUsers_Id",
            //            column: x => x.Id,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "KnowlegeArticles",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        ArticleId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        ArticleTitle = table.Column<string>(type: "varchar(200)", nullable: true),
            //        ArticleDetails = table.Column<string>(type: "text", nullable: true),
            //        HelpfulCount = table.Column<int>(type: "integer", nullable: false),
            //        NotHelpfulCount = table.Column<int>(type: "integer", nullable: false),
            //        CategoryId = table.Column<int>(type: "integer", nullable: false),
            //        HasImage = table.Column<bool>(type: "bool", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_KnowlegeArticles", x => x.ArticleId);
            //        table.ForeignKey(
            //            name: "FK_KnowlegeArticles_KnowlegeCategories_CategoryId",
            //            column: x => x.CategoryId,
            //            principalSchema: "dbo",
            //            principalTable: "KnowlegeCategories",
            //            principalColumn: "CategoryId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DialogMembers",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        MemberId = table.Column<int>(type: "serial", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        UserId = table.Column<string>(type: "text", nullable: true),
            //        Joined = table.Column<DateTime>(type: "timestamp", nullable: false),
            //        DialogId = table.Column<long>(type: "serial", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DialogMembers", x => x.MemberId);
            //        table.ForeignKey(
            //            name: "FK_DialogMembers_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_DialogMembers_MainInfoDialogs_DialogId",
            //            column: x => x.DialogId,
            //            principalSchema: "dbo",
            //            principalTable: "MainInfoDialogs",
            //            principalColumn: "DialogId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DialogMessages",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        MessageId = table.Column<int>(type: "serial", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        DialogId = table.Column<long>(type: "serial", nullable: false),
            //        Message = table.Column<string>(type: "text", nullable: true),
            //        Created = table.Column<DateTime>(type: "timestamp", nullable: false),
            //        UserId = table.Column<string>(type: "text", nullable: true),
            //        IsMyMessage = table.Column<bool>(type: "boolean", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DialogMessages", x => x.MessageId);
            //        table.ForeignKey(
            //            name: "FK_DialogMessages_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_DialogMessages_MainInfoDialogs_DialogId",
            //            column: x => x.DialogId,
            //            principalSchema: "dbo",
            //            principalTable: "MainInfoDialogs",
            //            principalColumn: "DialogId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_DialogMembers_DialogId",
            //    schema: "dbo",
            //    table: "DialogMembers",
            //    column: "DialogId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DialogMembers_UserId",
            //    schema: "dbo",
            //    table: "DialogMembers",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DialogMessages_DialogId",
            //    schema: "dbo",
            //    table: "DialogMessages",
            //    column: "DialogId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DialogMessages_UserId",
            //    schema: "dbo",
            //    table: "DialogMessages",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ExecutorStatistic_Id",
            //    schema: "dbo",
            //    table: "ExecutorStatistic",
            //    column: "Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_KnowlegeArticles_CategoryId",
            //    schema: "dbo",
            //    table: "KnowlegeArticles",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_State_Id",
            //    schema: "dbo",
            //    table: "State",
            //    column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AnswerVariants",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "DialogMembers",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "DialogMessages",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "ExecutorStatistic",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "KnowlegeArticles",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "PopularArticles",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "Questions",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "Responds",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "RespondTemplates",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "State",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "TaskCategories",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "Tasks",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "TaskStatuses",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "TaskTypes",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "MainInfoDialogs",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "KnowlegeCategories",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");
        }
    }
}
