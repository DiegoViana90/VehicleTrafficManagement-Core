using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class AjustFineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Fines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Fines",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
