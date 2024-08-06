using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class AlterFineTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountedFineAmount",
                table: "Fines",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "InterestFineAmount",
                table: "Fines",
                type: "numeric",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestFineAmount",
                table: "Fines");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountedFineAmount",
                table: "Fines",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }
    }
}
