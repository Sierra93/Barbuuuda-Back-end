using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Migrations
{
    public partial class AddTableHeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_profile",
                schema: "dbo",
                table: "Headers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_profile",
                schema: "dbo",
                table: "Headers");
        }
    }
}
