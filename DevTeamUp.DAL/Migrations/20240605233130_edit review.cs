using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTeamUp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Projects_ProjectId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Evaluation",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Evaluation",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
