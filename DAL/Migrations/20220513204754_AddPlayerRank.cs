using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddPlayerRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_PlayerId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Matches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerId",
                table: "Matches",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_PlayerId",
                table: "Matches",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
