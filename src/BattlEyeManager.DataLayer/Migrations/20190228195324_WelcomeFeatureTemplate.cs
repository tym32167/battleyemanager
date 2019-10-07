using Microsoft.EntityFrameworkCore.Migrations;

namespace BattlEyeManager.DataLayer.Migrations
{
    public partial class WelcomeFeatureTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WelcomeFeatureTemplate",
                table: "Servers", maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WelcomeFeatureTemplate",
                table: "Servers");
        }
    }
}
