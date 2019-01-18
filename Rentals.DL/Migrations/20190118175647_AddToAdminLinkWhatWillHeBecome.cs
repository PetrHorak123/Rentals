using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentals.DL.Migrations
{
    public partial class AddToAdminLinkWhatWillHeBecome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WillBeAdmin",
                table: "AdminInvites",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WillBeEmployee",
                table: "AdminInvites",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WillBeAdmin",
                table: "AdminInvites");

            migrationBuilder.DropColumn(
                name: "WillBeEmployee",
                table: "AdminInvites");
        }
    }
}
