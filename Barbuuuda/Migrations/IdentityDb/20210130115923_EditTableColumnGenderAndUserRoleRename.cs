using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Migrations.IdentityDb
{
    public partial class EditTableColumnGenderAndUserRoleRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "AspNetUsers",
                newName: "UserRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "AspNetUsers",
                newName: "UserType");
        }
    }
}
