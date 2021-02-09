using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Migrations.IdentityDb
{
    public partial class ChangeFieldTypeGenderAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserRole",
                table: "AspNetUsers",
                type: "varchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1)");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "varchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserRole",
                table: "AspNetUsers",
                type: "character varying(1)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "character varying(1)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldNullable: true);
        }
    }
}
