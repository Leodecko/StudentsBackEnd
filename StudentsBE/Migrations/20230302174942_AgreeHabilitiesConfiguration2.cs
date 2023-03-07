using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentsBE.Migrations
{
    /// <inheritdoc />
    public partial class AgreeHabilitiesConfiguration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hability",
                columns: new[] { "Id", "Name", "SubjectId" },
                values: new object[,]
                {
                    { 1, "Out loud reading", 1 },
                    { 2, "Reading comprehension", 1 },
                    { 3, "Readability", 1 },
                    { 4, "Coherence", 1 },
                    { 5, "Sum", 2 },
                    { 6, "Subtraction", 2 },
                    { 7, "Multiplication", 2 },
                    { 8, "Division", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hability",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
