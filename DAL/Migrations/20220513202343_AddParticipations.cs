using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddParticipations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchPlayer");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Matches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartingRank = table.Column<int>(type: "INTEGER", nullable: false),
                    FinishingRank = table.Column<int>(type: "INTEGER", nullable: false),
                    RankDifference = table.Column<int>(type: "INTEGER", nullable: false),
                    HasWon = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participation_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participation_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerId",
                table: "Matches",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_MatchId",
                table: "Participation",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_PlayerId",
                table: "Participation",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_PlayerId",
                table: "Matches",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_PlayerId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Participation");

            migrationBuilder.DropIndex(
                name: "IX_Matches_PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Matches");

            migrationBuilder.CreateTable(
                name: "MatchPlayer",
                columns: table => new
                {
                    MatchesId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayer", x => new { x.MatchesId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Matches_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_PlayersId",
                table: "MatchPlayer",
                column: "PlayersId");
        }
    }
}
