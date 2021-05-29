using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Barbuuuda.Core.Migrations
{
    public partial class AddColumnScoreEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Invoices",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        ScoreId = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        UserId = table.Column<string>(type: "text", nullable: false),
            //        InvoiceAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
            //        Currency = table.Column<string>(type: "varchar(10)", nullable: false),
            //        ScoreNumber = table.Column<int>(type: "int", nullable: false),
            //        ScoreEmail = table.Column<string>(type: "varchar(500)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Invoices", x => x.ScoreId);
            //        table.ForeignKey(
            //            name: "FK_Invoices_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Invoices_UserId",
            //    schema: "dbo",
            //    table: "Invoices",
            //    column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Invoices",
            //    schema: "dbo");
        }
    }
}
