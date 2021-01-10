using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Barbuuuda.Migrations.PostgreDb
{
    public partial class ChangeColumnsTableTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskCategories_task_category_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStatuses_task_status_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskTypes_task_type_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_task_category_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_task_status_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_task_type_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "date_create_task",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "task_category_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "task_status_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "task_type_id",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "user_type",
                schema: "dbo",
                table: "Users",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_phone",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_password",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_login",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_icon",
                schema: "dbo",
                table: "Users",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_email",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "token",
                schema: "dbo",
                table: "Users",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "rating",
                schema: "dbo",
                table: "Users",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "patronymic",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_online",
                schema: "dbo",
                table: "Users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "nvarchar(5)");

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "dbo",
                table: "Users",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_register",
                schema: "dbo",
                table: "Users",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<int>(
                name: "count_positive",
                schema: "dbo",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "count_negative",
                schema: "dbo",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "type_name",
                schema: "dbo",
                table: "TaskTypes",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type_code",
                schema: "dbo",
                table: "TaskTypes",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status_name",
                schema: "dbo",
                table: "TaskStatuses",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status_code",
                schema: "dbo",
                table: "TaskStatuses",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "owner_id",
                schema: "dbo",
                table: "Tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "executor_id",
                schema: "dbo",
                table: "Tasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "count_views",
                schema: "dbo",
                table: "Tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "count_offers",
                schema: "dbo",
                table: "Tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "category_code",
                schema: "dbo",
                table: "Tasks",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "spec_code",
                schema: "dbo",
                table: "Tasks",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status_code",
                schema: "dbo",
                table: "Tasks",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "task_begda",
                schema: "dbo",
                table: "Tasks",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "task_endda",
                schema: "dbo",
                table: "Tasks",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "type_code",
                schema: "dbo",
                table: "Tasks",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                schema: "dbo",
                table: "TaskCategories",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "category_code",
                schema: "dbo",
                table: "TaskCategories",
                type: "varchar(100)",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskSpecializations",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "type_code",
                schema: "dbo",
                table: "TaskTypes");

            migrationBuilder.DropColumn(
                name: "status_code",
                schema: "dbo",
                table: "TaskStatuses");

            migrationBuilder.DropColumn(
                name: "category_code",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "spec_code",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "status_code",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "task_begda",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "task_endda",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "type_code",
                schema: "dbo",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "category_code",
                schema: "dbo",
                table: "TaskCategories");

            migrationBuilder.AlterColumn<string>(
                name: "user_type",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_phone",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_password",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_login",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_icon",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_email",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "token",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "rating",
                schema: "dbo",
                table: "Users",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<string>(
                name: "patronymic",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_online",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(5)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_register",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<int>(
                name: "count_positive",
                schema: "dbo",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "count_negative",
                schema: "dbo",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "type_name",
                schema: "dbo",
                table: "TaskTypes",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status_name",
                schema: "dbo",
                table: "TaskStatuses",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "owner_id",
                schema: "dbo",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "executor_id",
                schema: "dbo",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "count_views",
                schema: "dbo",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "count_offers",
                schema: "dbo",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_create_task",
                schema: "dbo",
                table: "Tasks",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "task_category_id",
                schema: "dbo",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "task_status_id",
                schema: "dbo",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "task_type_id",
                schema: "dbo",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                schema: "dbo",
                table: "TaskCategories",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_task_category_id",
                schema: "dbo",
                table: "Tasks",
                column: "task_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_task_status_id",
                schema: "dbo",
                table: "Tasks",
                column: "task_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_task_type_id",
                schema: "dbo",
                table: "Tasks",
                column: "task_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskCategories_task_category_id",
                schema: "dbo",
                table: "Tasks",
                column: "task_category_id",
                principalSchema: "dbo",
                principalTable: "TaskCategories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStatuses_task_status_id",
                schema: "dbo",
                table: "Tasks",
                column: "task_status_id",
                principalSchema: "dbo",
                principalTable: "TaskStatuses",
                principalColumn: "status_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskTypes_task_type_id",
                schema: "dbo",
                table: "Tasks",
                column: "task_type_id",
                principalSchema: "dbo",
                principalTable: "TaskTypes",
                principalColumn: "type_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
