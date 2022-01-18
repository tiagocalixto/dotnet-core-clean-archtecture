using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLittlePetShop.DataProvider.Migrations
{
    public partial class InitialMigration22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "pet",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "owner",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "contact",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "pet");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "owner");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "contact");
        }
    }
}
