using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitPlanner.Apis.Main.Migrations
{
    public partial class AddedWingBaseURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseUrl",
                table: "CAPWings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OverrideBaseUrl",
                table: "CAPSquadrons",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OverrideBaseUrl",
                table: "CAPGroups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OverrideBaseUrl",
                table: "CAPActivityAccounts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseUrl",
                table: "CAPWings");

            migrationBuilder.DropColumn(
                name: "OverrideBaseUrl",
                table: "CAPSquadrons");

            migrationBuilder.DropColumn(
                name: "OverrideBaseUrl",
                table: "CAPGroups");

            migrationBuilder.DropColumn(
                name: "OverrideBaseUrl",
                table: "CAPActivityAccounts");
        }
    }
}
