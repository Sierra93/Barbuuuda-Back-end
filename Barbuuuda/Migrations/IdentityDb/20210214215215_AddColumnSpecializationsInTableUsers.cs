using Barbuuuda.Models.User;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Migrations.IdentityDb
{
    public partial class AddColumnSpecializationsInTableUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ExecutorSpecialization[]>(
                name: "ExecutorSpecializations",
                table: "AspNetUsers",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutorSpecializations",
                table: "AspNetUsers");
        }
    }
}
