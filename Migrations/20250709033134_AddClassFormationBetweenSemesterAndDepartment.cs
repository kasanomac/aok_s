using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aok_s.Migrations
{
    /// <inheritdoc />
    public partial class AddClassFormationBetweenSemesterAndDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Semesters_SemesterId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "Departments",
                newName: "ClassFormationId");

            migrationBuilder.RenameColumn(
                name: "DepartmentName",
                table: "Departments",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_SemesterId",
                table: "Departments",
                newName: "IX_Departments_ClassFormationId");

            migrationBuilder.CreateTable(
                name: "ClassFormations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SemesterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassFormations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassFormations_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFormations_SemesterId",
                table: "ClassFormations",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_ClassFormations_ClassFormationId",
                table: "Departments",
                column: "ClassFormationId",
                principalTable: "ClassFormations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_ClassFormations_ClassFormationId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "ClassFormations");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Departments",
                newName: "DepartmentName");

            migrationBuilder.RenameColumn(
                name: "ClassFormationId",
                table: "Departments",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_ClassFormationId",
                table: "Departments",
                newName: "IX_Departments_SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Semesters_SemesterId",
                table: "Departments",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
