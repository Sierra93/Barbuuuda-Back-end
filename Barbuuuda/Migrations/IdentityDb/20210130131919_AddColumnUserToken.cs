using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Migrations.IdentityDb
{
    public partial class AddColumnUserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserToken",
                table: "AspNetUsers");
        }
    }
}
