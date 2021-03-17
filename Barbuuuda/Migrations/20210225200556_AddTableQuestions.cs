using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbuuuda.Migrations
{
    public partial class AddTableQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "work_id",
            //    schema: "dbo",
            //    table: "Works",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Whies",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "type_id",
            //    schema: "dbo",
            //    table: "TaskTypes",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "status_id",
            //    schema: "dbo",
            //    table: "TaskStatuses",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "category_id",
            //    schema: "dbo",
            //    table: "TaskCategories",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddColumn<string>(
            //    name: "url",
            //    schema: "dbo",
            //    table: "TaskCategories",
            //    type: "text",
            //    nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Privileges",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "log_id",
            //    schema: "dbo",
            //    table: "Logs",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "hope_id",
            //    schema: "dbo",
            //    table: "Hopes",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Headers",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "fon_id",
            //    schema: "dbo",
            //    table: "Fons",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Advantages",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.CreateTable(
            //    name: "AnswerVariants",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        AnswerVariantId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AnswerVariantText = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuestionId = table.Column<int>(type: "int", nullable: false),
            //        IsTrue = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AnswerVariants", x => x.AnswerVariantId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Questions",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        QuestionId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Questions", x => x.QuestionId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AnswerVariants",
            //    schema: "dbo");

            //migrationBuilder.DropTable(
            //    name: "Questions",
            //    schema: "dbo");

            //migrationBuilder.DropColumn(
            //    name: "url",
            //    schema: "dbo",
            //    table: "TaskCategories");

            //migrationBuilder.AlterColumn<int>(
            //    name: "work_id",
            //    schema: "dbo",
            //    table: "Works",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Whies",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "type_id",
            //    schema: "dbo",
            //    table: "TaskTypes",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "status_id",
            //    schema: "dbo",
            //    table: "TaskStatuses",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "category_id",
            //    schema: "dbo",
            //    table: "TaskCategories",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Privileges",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "log_id",
            //    schema: "dbo",
            //    table: "Logs",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "hope_id",
            //    schema: "dbo",
            //    table: "Hopes",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Headers",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "fon_id",
            //    schema: "dbo",
            //    table: "Fons",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    schema: "dbo",
            //    table: "Advantages",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
