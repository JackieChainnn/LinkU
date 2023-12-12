using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkU.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantSkill_AspNetUsers_ApplicantID",
                table: "ApplicantSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantSkill_Skill_SkillID",
                table: "ApplicantSkill");

            migrationBuilder.DropTable(
                name: "Requirement");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Documentation",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "JobOpeningSkill",
                newName: "SkillID");

            migrationBuilder.RenameColumn(
                name: "JobOpeningId",
                table: "JobOpeningSkill",
                newName: "JobOpeningID");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentationPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOpeningSkill_SkillID",
                table: "JobOpeningSkill",
                column: "SkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantSkill_AspNetUsers_ApplicantID",
                table: "ApplicantSkill",
                column: "ApplicantID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantSkill_Skill_SkillID",
                table: "ApplicantSkill",
                column: "SkillID",
                principalTable: "Skill",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpeningSkill_JobOpening_JobOpeningID",
                table: "JobOpeningSkill",
                column: "JobOpeningID",
                principalTable: "JobOpening",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpeningSkill_Skill_SkillID",
                table: "JobOpeningSkill",
                column: "SkillID",
                principalTable: "Skill",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantSkill_AspNetUsers_ApplicantID",
                table: "ApplicantSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantSkill_Skill_SkillID",
                table: "ApplicantSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOpeningSkill_JobOpening_JobOpeningID",
                table: "JobOpeningSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOpeningSkill_Skill_SkillID",
                table: "JobOpeningSkill");

            migrationBuilder.DropIndex(
                name: "IX_JobOpeningSkill_SkillID",
                table: "JobOpeningSkill");

            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DocumentationPath",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SkillID",
                table: "JobOpeningSkill",
                newName: "SkillId");

            migrationBuilder.RenameColumn(
                name: "JobOpeningID",
                table: "JobOpeningSkill",
                newName: "JobOpeningId");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Documentation",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    JobOpeningID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SkillID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => new { x.JobOpeningID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_Requirement_JobOpening_JobOpeningID",
                        column: x => x.JobOpeningID,
                        principalTable: "JobOpening",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Requirement_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_SkillID",
                table: "Requirement",
                column: "SkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantSkill_AspNetUsers_ApplicantID",
                table: "ApplicantSkill",
                column: "ApplicantID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantSkill_Skill_SkillID",
                table: "ApplicantSkill",
                column: "SkillID",
                principalTable: "Skill",
                principalColumn: "ID");
        }
    }
}
