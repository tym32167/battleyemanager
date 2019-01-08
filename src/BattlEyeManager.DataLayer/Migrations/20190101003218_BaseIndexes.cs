using Microsoft.EntityFrameworkCore.Migrations;

namespace BattlEyeManager.DataLayer.Migrations
{
    public partial class BaseIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SteamId",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "ChatMessages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSessions_EndDate",
                table: "PlayerSessions",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSessions_StartDate",
                table: "PlayerSessions",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Players_IP",
                table: "Players",
                column: "IP");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Players_SteamId",
                table: "Players",
                column: "SteamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_Date",
                table: "ChatMessages",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_Text",
                table: "ChatMessages",
                column: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerSessions_EndDate",
                table: "PlayerSessions");

            migrationBuilder.DropIndex(
                name: "IX_PlayerSessions_StartDate",
                table: "PlayerSessions");

            migrationBuilder.DropIndex(
                name: "IX_Players_IP",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Name",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_SteamId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_Date",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_Text",
                table: "ChatMessages");

            migrationBuilder.AlterColumn<string>(
                name: "SteamId",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "ChatMessages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
