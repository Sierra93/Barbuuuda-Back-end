using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Core.Migrations
{
    public partial class AddColumnScoreEmailV11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "ScoreEmail",
            //    schema: "dbo",
            //    table: "Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScoreEmail",
                schema: "dbo",
                table: "Invoices",
                type: "varchar(500)",
                nullable: true);
        }
    }
}
