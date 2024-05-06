using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTeamUp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillUser_Skills_SkillId",
                table: "SkillUser");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "SkillUser",
                newName: "SkillsId");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "shortDescription",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GitHubLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Projects_ProjectId",
                table: "Reviews",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillUser_Skills_SkillsId",
                table: "SkillUser",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Projects_ProjectId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillUser_Skills_SkillsId",
                table: "SkillUser");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "shortDescription",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GitHubLink",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "SkillUser",
                newName: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillUser_Skills_SkillId",
                table: "SkillUser",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
