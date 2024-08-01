using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class AlterFineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FineNumber = table.Column<string>(type: "text", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    FineDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FineDueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EnforcingAgency = table.Column<int>(type: "integer", nullable: false),
                    FineLocation = table.Column<string>(type: "text", nullable: false),
                    FineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountedFineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    FinalFineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    FineStatus = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineId);
                    table.ForeignKey(
                        name: "FK_Fines_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fines_VehicleId",
                table: "Fines",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DiscountedFineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    EnforcingAgency = table.Column<int>(type: "integer", nullable: false),
                    FinalFineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    FineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    FineDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FineDueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FineLocation = table.Column<string>(type: "text", nullable: false),
                    FineNumber = table.Column<string>(type: "text", nullable: false),
                    FineStatus = table.Column<int>(type: "integer", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineId);
                    table.ForeignKey(
                        name: "FK_Fines_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fines_VehicleId",
                table: "Fines",
                column: "VehicleId");
        }
    }
}
