using Microsoft.EntityFrameworkCore.Migrations;

namespace BattlEyeManager.DataLayer.Migrations
{
    public partial class WelcomeGreater50MessageTemplateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WelcomeGreater50MessageTemplate",
                table: "Servers",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WelcomeGreater50MessageTemplate",
                table: "Servers");
        }
    }
}
