using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLittlePetShop.DataProvider.Migrations
{
    public partial class InitialMigration42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "pet",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "owner",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "pet",
                newName: "deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "owner",
                newName: "deleted");
        }
    }
}
