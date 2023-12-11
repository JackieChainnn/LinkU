using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkU.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBirthdayDocumentationForCommon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resume",
                table: "AspNetUsers",
                newName: "Documentation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Documentation",
                table: "AspNetUsers",
                newName: "Resume");
        }
    }
}
