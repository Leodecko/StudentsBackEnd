using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentsBE.Migrations
{
    /// <inheritdoc />
    public partial class dbModelFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectHability_Subject_SubjectId",
                table: "StudentSubjectHability");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjectHability_SubjectId",
                table: "StudentSubjectHability");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "StudentSubjectHability");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Hability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Spanish" },
                    { 2, "Math" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hability_SubjectId",
                table: "Hability",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hability_Subject_SubjectId",
                table: "Hability",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hability_Subject_SubjectId",
                table: "Hability");

            migrationBuilder.DropIndex(
                name: "IX_Hability_SubjectId",
                table: "Hability");

            migrationBuilder.DeleteData(
                table: "Subject",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subject",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Hability");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "StudentSubjectHability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectHability_SubjectId",
                table: "StudentSubjectHability",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectHability_Subject_SubjectId",
                table: "StudentSubjectHability",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
