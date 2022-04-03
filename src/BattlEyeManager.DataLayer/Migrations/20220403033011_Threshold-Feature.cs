using Microsoft.EntityFrameworkCore.Migrations;

namespace BattlEyeManager.DataLayer.Migrations
{
    public partial class ThresholdFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ThresholdFeatureEnabled",
                table: "Servers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ThresholdFeatureMessageTemplate",
                table: "Servers",
                type: "varchar(255)",
                defaultValue: "Server is full now, try again later.",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ThresholdMinHoursCap",
                table: "Servers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThresholdFeatureEnabled",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "ThresholdFeatureMessageTemplate",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "ThresholdMinHoursCap",
                table: "Servers");
        }
    }
}
