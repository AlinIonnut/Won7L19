using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Won7E1.Migrations
{
    /// <inheritdoc />
    public partial class NamingNotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Students_StudentId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Subject_SubjectId",
                table: "Mark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mark",
                table: "Mark");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "Mark",
                newName: "Marks");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_SubjectId",
                table: "Marks",
                newName: "IX_Marks_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_StudentId",
                table: "Marks",
                newName: "IX_Marks_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marks",
                table: "Marks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Subjects_SubjectId",
                table: "Marks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Subjects_SubjectId",
                table: "Marks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marks",
                table: "Marks");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "Marks",
                newName: "Mark");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_SubjectId",
                table: "Mark",
                newName: "IX_Mark_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_StudentId",
                table: "Mark",
                newName: "IX_Mark_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mark",
                table: "Mark",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Students_StudentId",
                table: "Mark",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Subject_SubjectId",
                table: "Mark",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");
        }
    }
}
