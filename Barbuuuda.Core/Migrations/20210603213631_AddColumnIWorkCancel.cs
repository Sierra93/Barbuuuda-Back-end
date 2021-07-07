using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Core.Migrations
{
    public partial class AddColumnIWorkCancel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWorkCancel",
                schema: "dbo",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "IsWorkCancel",
            //    schema: "dbo",
            //    table: "Tasks");
        }
    }
}
