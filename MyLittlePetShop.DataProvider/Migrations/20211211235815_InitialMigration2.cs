using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLittlePetShop.DataProvider.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerDbOwnerId",
                table: "pet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerDbOwnerId",
                table: "contact",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pet_OwnerDbOwnerId",
                table: "pet",
                column: "OwnerDbOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_contact_OwnerDbOwnerId",
                table: "contact",
                column: "OwnerDbOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_owner_OwnerDbOwnerId",
                table: "contact",
                column: "OwnerDbOwnerId",
                principalTable: "owner",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pet_owner_OwnerDbOwnerId",
                table: "pet",
                column: "OwnerDbOwnerId",
                principalTable: "owner",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_owner_OwnerDbOwnerId",
                table: "contact");

            migrationBuilder.DropForeignKey(
                name: "FK_pet_owner_OwnerDbOwnerId",
                table: "pet");

            migrationBuilder.DropIndex(
                name: "IX_pet_OwnerDbOwnerId",
                table: "pet");

            migrationBuilder.DropIndex(
                name: "IX_contact_OwnerDbOwnerId",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "OwnerDbOwnerId",
                table: "pet");

            migrationBuilder.DropColumn(
                name: "OwnerDbOwnerId",
                table: "contact");
        }
    }
}
