using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentals.DL.Migrations
{
    public partial class AddRentalContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Rentals",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Rentals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Rentals");
        }
    }
}
