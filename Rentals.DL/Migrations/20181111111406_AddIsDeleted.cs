using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentals.DL.Migrations
{
    public partial class AddIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_UniqueIndentifier",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UniqueIndentifier",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ItemTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentifier",
                table: "Items",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UniqueIdentifier",
                table: "Items",
                column: "UniqueIdentifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_UniqueIdentifier",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UniqueIdentifier",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "UniqueIndentifier",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UniqueIndentifier",
                table: "Items",
                column: "UniqueIndentifier",
                unique: true,
                filter: "[UniqueIndentifier] IS NOT NULL");
        }
    }
}
