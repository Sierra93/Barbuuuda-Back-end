using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Barbuuuda.Migrations.PostgreDb
{
    public partial class AddTablePosgreTaskv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "TaskCategories",
                schema: "dbo",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                schema: "dbo",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status_name = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatuses", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                schema: "dbo",
                columns: table => new
                {
                    type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.type_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_login = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    user_password = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    user_email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    user_type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    user_phone = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    patronymic = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    count_positive = table.Column<int>(type: "int", nullable: false),
                    count_negative = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<double>(type: "float", nullable: false),
                    is_online = table.Column<bool>(type: "nvarchar(5)", nullable: false),
                    date_register = table.Column<DateTime>(type: "nvarchar(100)", nullable: false)
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
                    task_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    owner_id = table.Column<int>(type: "int", nullable: false),
                    executor_id = table.Column<int>(type: "int", nullable: true),
                    date_create_task = table.Column<DateTime>(type: "datetime", nullable: false),
                    count_offers = table.Column<int>(type: "int", nullable: false),
                    count_views = table.Column<int>(type: "int", nullable: false),
                    task_type_id = table.Column<int>(type: "int", nullable: false),
                    task_status_id = table.Column<int>(type: "int", nullable: false),
                    task_category_id = table.Column<int>(type: "int", nullable: false),
                    task_price = table.Column<decimal>(type: "money", nullable: true),
                    task_title = table.Column<string>(type: "text", nullable: true),
                    task_detail = table.Column<string>(type: "text", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TaskCategories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TaskStatuses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TaskTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
