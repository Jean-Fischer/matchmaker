using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class SeedMorePlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Nickname", "Rank" },
                values: new object[] { "Jean", 1100 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nickname", "Rank" },
                values: new object[] { "Martin", 1200 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Nickname", "Rank" },
                values: new object[] { 3, "Greg", 1200 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Nickname", "Rank" },
                values: new object[] { 4, "Cosmin", 1200 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Nickname", "Rank" },
                values: new object[] { 5, "Kevin", 1200 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Nickname", "Rank" },
                values: new object[] { "Testos", 0 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nickname", "Rank" },
                values: new object[] { "Testas", 0 });
        }
    }
}
