using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Core.Migrations
{
    public partial class AddColumnIsPayTableTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPay",
                schema: "dbo",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.AlterColumn<int>(
            //    name: "ScoreNumber",
            //    schema: "dbo",
            //    table: "Invoices",
            //    type: "int4",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "IsPay",
            //    schema: "dbo",
            //    table: "Tasks");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ScoreNumber",
            //    schema: "dbo",
            //    table: "Invoices",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int4",
            //    oldNullable: true);
        }
    }
}
