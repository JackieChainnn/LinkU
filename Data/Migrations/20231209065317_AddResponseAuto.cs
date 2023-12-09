using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkU.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResponseAuto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "SupportResponse",
                type: "date",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "SupportResponse",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");
        }
    }
}
