using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentals.DL.Migrations
{
    public partial class AddOpeningHoursAndAccesories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndsAt",
                table: "Rentals",
                nullable: false,
                defaultValue: new TimeSpan(0, 8, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "MinTimeUnit",
                table: "Rentals",
                nullable: false,
                defaultValue: 30);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartsAt",
                table: "Rentals",
                nullable: false,
                defaultValue: new TimeSpan(0, 15, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    AccesoryToId = table.Column<int>(nullable: false),
                    AccesoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => new { x.AccesoryId, x.AccesoryToId });
                    table.ForeignKey(
                        name: "FK_Accessories_ItemTypes_AccesoryId",
                        column: x => x.AccesoryId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accessories_ItemTypes_AccesoryToId",
                        column: x => x.AccesoryToId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_AccesoryToId",
                table: "Accessories",
                column: "AccesoryToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropColumn(
                name: "EndsAt",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MinTimeUnit",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "StartsAt",
                table: "Rentals");
        }
    }
}
