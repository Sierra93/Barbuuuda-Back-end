using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Core.Migrations
{
    public partial class RemoveColumnScoreUsersProdactionDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                schema: "dbo",
                table: "ExecutorStatistic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<decimal>(
            //    name: "Score",
            //    schema: "dbo",
            //    table: "ExecutorStatistic",
            //    type: "numeric(12,2)",
            //    nullable: false,
            //    defaultValue: 0m);
        }
    }
}
