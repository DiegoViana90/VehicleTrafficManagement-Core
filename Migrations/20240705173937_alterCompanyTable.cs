using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class alterCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyInformationId",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CompanyInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CEP = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    PropertyNumber = table.Column<string>(type: "text", nullable: false),
                    District = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    AdressComplement = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Observations = table.Column<string>(type: "text", nullable: false),
                    CompanyStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInformation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyInformationId",
                table: "Companies",
                column: "CompanyInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyInformation_CompanyInformationId",
                table: "Companies",
                column: "CompanyInformationId",
                principalTable: "CompanyInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyInformation_CompanyInformationId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyInformation");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CompanyInformationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyInformationId",
                table: "Companies");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Companies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
