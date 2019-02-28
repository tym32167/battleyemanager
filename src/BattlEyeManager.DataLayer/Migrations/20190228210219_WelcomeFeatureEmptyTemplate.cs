using Microsoft.EntityFrameworkCore.Migrations;

namespace BattlEyeManager.DataLayer.Migrations
{
    public partial class WelcomeFeatureEmptyTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WelcomeFeatureEmptyTemplate",
                table: "Servers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WelcomeFeatureEmptyTemplate",
                table: "Servers");
        }
    }
}
