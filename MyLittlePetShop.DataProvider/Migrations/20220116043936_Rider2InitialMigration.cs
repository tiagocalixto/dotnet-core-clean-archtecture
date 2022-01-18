using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLittlePetShop.DataProvider.Migrations
{
    public partial class Rider2InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "login");

            migrationBuilder.AddPrimaryKey(
                name: "PK_login",
                table: "login",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_login",
                table: "login");

            migrationBuilder.RenameTable(
                name: "login",
                newName: "user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");
        }
    }
}
