using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aok_s.Migrations
{
    /// <inheritdoc />
    public partial class AddPeriodAndTeacherToClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Classes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "Classes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "Classes");
        }
    }
}
