using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduAdmin.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnNameOnGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Grades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Grades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
