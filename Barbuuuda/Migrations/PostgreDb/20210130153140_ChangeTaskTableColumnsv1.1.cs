using System;
using Barbuuuda.Models.Task;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Barbuuuda.Migrations.PostgreDb
{
    public partial class ChangeTaskTableColumnsv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_executor_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_owner_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskSpecializations",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_executor_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_owner_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "token",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "user_email",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "user_login",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "user_phone",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "user_type",
                schema: "dbo",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "type_code",
                schema: "dbo",
                table: "Tasks",
                newName: "TypeCode");

            migrationBuilder.RenameColumn(
                name: "task_title",
                schema: "dbo",
                table: "Tasks",
                newName: "TaskTitle");

            migrationBuilder.RenameColumn(
                name: "task_price",
                schema: "dbo",
                table: "Tasks",
                newName: "TaskPrice");

            migrationBuilder.RenameColumn(
                name: "task_endda",
                schema: "dbo",
                table: "Tasks",
                newName: "TaskEndda");

            migrationBuilder.RenameColumn(
                name: "task_detail",
                schema: "dbo",
                table: "Tasks",
                newName: "TaskDetail");

            migrationBuilder.RenameColumn(
                name: "task_begda",
                schema: "dbo",
                table: "Tasks",
                newName: "TaskBegda");

            migrationBuilder.RenameColumn(
                name: "status_code",
                schema: "dbo",
                table: "Tasks",
                newName: "StatusCode");

            migrationBuilder.RenameColumn(
                name: "spec_code",
                schema: "dbo",
                table: "Tasks",
                newName: "SpecCode");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                schema: "dbo",
                table: "Tasks",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "executor_id",
                schema: "dbo",
                table: "Tasks",
                newName: "ExecutorId");

            migrationBuilder.RenameColumn(
                name: "count_views",
                schema: "dbo",
                table: "Tasks",
                newName: "CountViews");

            migrationBuilder.RenameColumn(
                name: "count_offers",
                schema: "dbo",
                table: "Tasks",
                newName: "CountOffers");

            migrationBuilder.RenameColumn(
                name: "category_code",
                schema: "dbo",
                table: "Tasks",
                newName: "CategoryCode");

            migrationBuilder.RenameColumn(
                name: "task_id",
                schema: "dbo",
                table: "Tasks",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "AspNetUsers",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "AspNetUsers",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "user_password",
                table: "AspNetUsers",
                newName: "UserPassword");

            migrationBuilder.RenameColumn(
                name: "user_icon",
                table: "AspNetUsers",
                newName: "UserIcon");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "is_online",
                table: "AspNetUsers",
                newName: "IsOnline");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "date_register",
                table: "AspNetUsers",
                newName: "DateRegister");

            migrationBuilder.RenameColumn(
                name: "count_positive",
                table: "AspNetUsers",
                newName: "CountPositive");

            migrationBuilder.RenameColumn(
                name: "count_negative",
                table: "AspNetUsers",
                newName: "CountNegative");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "AspNetUsers",
                newName: "Age");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaskPrice",
                schema: "dbo",
                table: "Tasks",
                type: "numeric(12,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                schema: "dbo",
                table: "Tasks",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ExecutorId",
                schema: "dbo",
                table: "Tasks",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<Specialization[]>(
                name: "specializations",
                schema: "dbo",
                table: "TaskCategories",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "url",
                schema: "dbo",
                table: "TaskCategories",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "AspNetUsers",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<string>(
                name: "UserIcon",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AboutInfo",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "varchar(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Plan",
                table: "AspNetUsers",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "AspNetUsers",
                type: "numeric(12,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "AspNetUsers",
                type: "varchar(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "specializations",
                schema: "dbo",
                table: "TaskCategories");

            migrationBuilder.DropColumn(
                name: "url",
                schema: "dbo",
                table: "TaskCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AboutInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserToken",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "TypeCode",
                schema: "dbo",
                table: "Tasks",
                newName: "type_code");

            migrationBuilder.RenameColumn(
                name: "TaskTitle",
                schema: "dbo",
                table: "Tasks",
                newName: "task_title");

            migrationBuilder.RenameColumn(
                name: "TaskPrice",
                schema: "dbo",
                table: "Tasks",
                newName: "task_price");

            migrationBuilder.RenameColumn(
                name: "TaskEndda",
                schema: "dbo",
                table: "Tasks",
                newName: "task_endda");

            migrationBuilder.RenameColumn(
                name: "TaskDetail",
                schema: "dbo",
                table: "Tasks",
                newName: "task_detail");

            migrationBuilder.RenameColumn(
                name: "TaskBegda",
                schema: "dbo",
                table: "Tasks",
                newName: "task_begda");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                schema: "dbo",
                table: "Tasks",
                newName: "status_code");

            migrationBuilder.RenameColumn(
                name: "SpecCode",
                schema: "dbo",
                table: "Tasks",
                newName: "spec_code");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "dbo",
                table: "Tasks",
                newName: "owner_id");

            migrationBuilder.RenameColumn(
                name: "ExecutorId",
                schema: "dbo",
                table: "Tasks",
                newName: "executor_id");

            migrationBuilder.RenameColumn(
                name: "CountViews",
                schema: "dbo",
                table: "Tasks",
                newName: "count_views");

            migrationBuilder.RenameColumn(
                name: "CountOffers",
                schema: "dbo",
                table: "Tasks",
                newName: "count_offers");

            migrationBuilder.RenameColumn(
                name: "CategoryCode",
                schema: "dbo",
                table: "Tasks",
                newName: "category_code");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                schema: "dbo",
                table: "Tasks",
                newName: "task_id");

            migrationBuilder.RenameColumn(
                name: "Rating",
                schema: "dbo",
                table: "Users",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                schema: "dbo",
                table: "Users",
                newName: "patronymic");

            migrationBuilder.RenameColumn(
                name: "UserPassword",
                schema: "dbo",
                table: "Users",
                newName: "user_password");

            migrationBuilder.RenameColumn(
                name: "UserIcon",
                schema: "dbo",
                table: "Users",
                newName: "user_icon");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "dbo",
                table: "Users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "IsOnline",
                schema: "dbo",
                table: "Users",
                newName: "is_online");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "dbo",
                table: "Users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "DateRegister",
                schema: "dbo",
                table: "Users",
                newName: "date_register");

            migrationBuilder.RenameColumn(
                name: "CountPositive",
                schema: "dbo",
                table: "Users",
                newName: "count_positive");

            migrationBuilder.RenameColumn(
                name: "CountNegative",
                schema: "dbo",
                table: "Users",
                newName: "count_negative");

            migrationBuilder.RenameColumn(
                name: "Age",
                schema: "dbo",
                table: "Users",
                newName: "user_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "task_price",
                schema: "dbo",
                table: "Tasks",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "owner_id",
                schema: "dbo",
                table: "Tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AlterColumn<int>(
                name: "executor_id",
                schema: "dbo",
                table: "Tasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "rating",
                schema: "dbo",
                table: "Users",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "user_icon",
                schema: "dbo",
                table: "Users",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                schema: "dbo",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "token",
                schema: "dbo",
                table: "Users",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_login",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_phone",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_type",
                schema: "dbo",
                table: "Users",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "dbo",
                table: "Users",
                column: "user_id");

            migrationBuilder.CreateTable(
                name: "TaskSpecializations",
                schema: "dbo",
                columns: table => new
                {
                    spec_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    spec_code = table.Column<string>(type: "varchar(100)", nullable: true),
                    spec_name = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSpecializations", x => x.spec_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_executor_id",
                schema: "dbo",
                table: "Tasks",
                column: "executor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_owner_id",
                schema: "dbo",
                table: "Tasks",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_executor_id",
                schema: "dbo",
                table: "Tasks",
                column: "executor_id",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_owner_id",
                schema: "dbo",
                table: "Tasks",
                column: "owner_id",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
