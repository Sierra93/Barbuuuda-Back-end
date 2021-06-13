//using Microsoft.EntityFrameworkCore.Migrations;
//using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

//namespace Barbuuuda.Core.Migrations
//{
//    public partial class AddTableCanceledInvities1 : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AlterColumn<string>(
//                name: "OwnerId",
//                schema: "dbo",
//                table: "Tasks",
//                type: "varchar(150)",
//                nullable: true,
//                oldClrType: typeof(string),
//                oldType: "varchar(150)");

//            migrationBuilder.CreateTable(
//                name: "CanceledInvities",
//                schema: "dbo",
//                columns: table => new
//                {
//                    CanceledInviteId = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                    ExecutorId = table.Column<string>(type: "text", nullable: true),
//                    UserId = table.Column<string>(type: "text", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_CanceledInvities", x => x.CanceledInviteId);
//                    table.ForeignKey(
//                        name: "FK_CanceledInvities_AspNetUsers_UserId",
//                        column: x => x.UserId,
//                        principalTable: "AspNetUsers",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Restrict);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_CanceledInvities_UserId",
//                schema: "dbo",
//                table: "CanceledInvities",
//                column: "UserId");
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "CanceledInvities",
//                schema: "dbo");

//            migrationBuilder.AlterColumn<string>(
//                name: "OwnerId",
//                schema: "dbo",
//                table: "Tasks",
//                type: "varchar(150)",
//                nullable: false,
//                defaultValue: "",
//                oldClrType: typeof(string),
//                oldType: "varchar(150)",
//                oldNullable: true);
//        }
//    }
//}
