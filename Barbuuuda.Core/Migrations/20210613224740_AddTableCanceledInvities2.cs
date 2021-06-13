using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Core.Migrations
{
    public partial class AddTableCanceledInvities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaskId",
                schema: "dbo",
                table: "CanceledInvities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "TaskId1",
                schema: "dbo",
                table: "CanceledInvities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CanceledInvities_TaskId1",
                schema: "dbo",
                table: "CanceledInvities",
                column: "TaskId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CanceledInvities_Tasks_TaskId1",
                schema: "dbo",
                table: "CanceledInvities",
                column: "TaskId1",
                principalSchema: "dbo",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CanceledInvities_Tasks_TaskId1",
            //    schema: "dbo",
            //    table: "CanceledInvities");

            //migrationBuilder.DropIndex(
            //    name: "IX_CanceledInvities_TaskId1",
            //    schema: "dbo",
            //    table: "CanceledInvities");

            //migrationBuilder.DropColumn(
            //    name: "TaskId",
            //    schema: "dbo",
            //    table: "CanceledInvities");

            //migrationBuilder.DropColumn(
            //    name: "TaskId1",
            //    schema: "dbo",
            //    table: "CanceledInvities");
        }
    }
}
