using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsBE.Migrations
{
    /// <inheritdoc />
    public partial class fixingstudentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SudentId",
                table: "StudentSubjectHability");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SudentId",
                table: "StudentSubjectHability",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
