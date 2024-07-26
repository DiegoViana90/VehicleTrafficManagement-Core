using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class alterVehicleTableCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "Vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CompaniesId",
                table: "Vehicles",
                column: "CompaniesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Companies_CompaniesId",
                table: "Vehicles",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "CompaniesId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Companies_CompaniesId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CompaniesId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "Vehicles");
        }
    }
}
