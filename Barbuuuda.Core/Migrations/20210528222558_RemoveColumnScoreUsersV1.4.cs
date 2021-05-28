using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Core.Migrations
{
    public partial class RemoveColumnScoreUsersV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<decimal>(
            //    name: "Score",
            //    table: "AspNetUsers",
            //    type: "numeric(12,2)",
            //    nullable: true);
        }
    }
}
