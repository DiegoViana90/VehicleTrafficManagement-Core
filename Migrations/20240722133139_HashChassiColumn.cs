using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class HashChassiColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashedChassi",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedChassi",
                table: "Vehicles");
        }
    }
}
