using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSpot.Infrastructure.DAL.Migrations
{
    public partial class Adding_Capacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "WeeklyParkingSpots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "WeeklyParkingSpots");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Reservations");
        }
    }
}
