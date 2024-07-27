using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class alterVehicleTableRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyRelated",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyRelated",
                table: "Companies");
        }
    }
}
