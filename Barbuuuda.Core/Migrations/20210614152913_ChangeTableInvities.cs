using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Barbuuuda.Core.Migrations
{
    public partial class ChangeTableInvities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Invities",
            //    schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Invities",
                schema: "dbo",
                columns: table => new
                {
                    InviteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<long>(type: "bigint", nullable: false),
                    ExecutorId = table.Column<string>(type: "text", nullable: true),
                    IsAccept = table.Column<bool>(type: "boolean", nullable: false),
                    IsCancel = table.Column<bool>(type: "boolean", nullable: false),
                    TaskId1 = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invities", x => x.InviteId);
                    table.ForeignKey(
                        name: "FK_Invities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invities_Tasks_TaskId1",
                        column: x => x.TaskId1,
                        principalSchema: "dbo",
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invities_TaskId1",
                schema: "dbo",
                table: "Invities",
                column: "TaskId1");

            migrationBuilder.CreateIndex(
                name: "IX_Invities_UserId",
                schema: "dbo",
                table: "Invities",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Invities",
            //    schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Invities",
                schema: "dbo",
                columns: table => new
                {
                    CanceledInviteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExecutorId = table.Column<string>(type: "text", nullable: true),
                    TaskId = table.Column<long>(type: "bigint", nullable: false),
                    TaskId1 = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invities", x => x.CanceledInviteId);
                    table.ForeignKey(
                        name: "FK_Invities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invities_Tasks_TaskId1",
                        column: x => x.TaskId1,
                        principalSchema: "dbo",
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invities_TaskId1",
                schema: "dbo",
                table: "Invities",
                column: "TaskId1");

            migrationBuilder.CreateIndex(
                name: "IX_Invities_UserId",
                schema: "dbo",
                table: "Invities",
                column: "UserId");
        }
    }
}
