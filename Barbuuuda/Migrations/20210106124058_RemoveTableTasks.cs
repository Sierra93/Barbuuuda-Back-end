using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Migrations
{
    public partial class RemoveTableTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "work_id",
                schema: "dbo",
                table: "Works",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Whies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "type_name",
                schema: "dbo",
                table: "TaskTypes",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                schema: "dbo",
                table: "TaskTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.AlterColumn<int>(
                name: "status_id",
                schema: "dbo",
                table: "TaskStatuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "status_code",
                schema: "dbo",
                table: "TaskStatuses",
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

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "dbo",
                table: "TaskCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "category_code",
                schema: "dbo",
                table: "TaskCategories",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Privileges",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Headers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "fon_id",
                schema: "dbo",
                table: "Fons",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Advantages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "TaskCategories");

            migrationBuilder.AlterColumn<int>(
                name: "work_id",
                schema: "dbo",
                table: "Works",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Whies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "type_name",
                schema: "dbo",
                table: "TaskTypes",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                schema: "dbo",
                table: "TaskTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

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
                name: "status_id",
                schema: "dbo",
                table: "TaskStatuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                schema: "dbo",
                table: "TaskCategories",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "dbo",
                table: "TaskCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Privileges",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Headers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "fon_id",
                schema: "dbo",
                table: "Fons",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "dbo",
                table: "Advantages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    count_negative = table.Column<int>(type: "int", nullable: false),
                    count_positive = table.Column<int>(type: "int", nullable: false),
                    date_register = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    is_online = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    patronymic = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    rating = table.Column<double>(type: "float", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    user_icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_login = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    user_password = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    user_phone = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    user_type = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "dbo",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false),
                    count_offers = table.Column<int>(type: "int", nullable: false),
                    count_views = table.Column<int>(type: "int", nullable: false),
                    date_create_task = table.Column<DateTime>(type: "datetime", nullable: false),
                    executor_id = table.Column<int>(type: "int", nullable: true),
                    owner_id = table.Column<int>(type: "int", nullable: false),
                    task_category_id = table.Column<int>(type: "int", nullable: false),
                    task_detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    task_price = table.Column<decimal>(type: "money", nullable: true),
                    task_status_id = table.Column<int>(type: "int", nullable: false),
                    task_title = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    task_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskCategories_task_category_id",
                        column: x => x.task_category_id,
                        principalSchema: "dbo",
                        principalTable: "TaskCategories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStatuses_task_status_id",
                        column: x => x.task_status_id,
                        principalSchema: "dbo",
                        principalTable: "TaskStatuses",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskTypes_task_type_id",
                        column: x => x.task_type_id,
                        principalSchema: "dbo",
                        principalTable: "TaskTypes",
                        principalColumn: "type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_executor_id",
                        column: x => x.executor_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_owner_id",
                        column: x => x.owner_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
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
        }
    }
}
