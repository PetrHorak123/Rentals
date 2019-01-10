using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentals.DL.Migrations
{
    public partial class AddBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Basket",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Basket",
                table: "AspNetUsers");
        }
    }
}
