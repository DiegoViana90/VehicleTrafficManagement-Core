using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class alterCNPJtoTaxNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Companies",
                newName: "TaxNumber");

            var dropFunctionSql = @"
            DROP FUNCTION IF EXISTS public.getcompanybycnpj;
            ";

            var createFunctionSql = @"
            CREATE OR REPLACE FUNCTION public.getcompanybytaxnumber(""paramTaxNumber"" text)
            RETURNS TABLE(
                ""CompaniesId"" int,
                ""Name"" text,
                ""TradeName"" text,
                ""TaxNumber"" text,
                ""CEP"" text,
                ""Street"" text,
                ""PropertyNumber"" text,
                ""District"" text,
                ""City"" text,
                ""State"" text,
                ""Country"" text,
                ""AdressComplement"" text,
                ""PhoneNumber"" text,
                ""Email"" text,
                ""Observations"" text,
                ""CompanyStatus"" int,
                ""CompanyInformationId"" int
            ) 
            LANGUAGE 'sql'
            COST 100
            VOLATILE PARALLEL UNSAFE
            ROWS 1000
            AS $BODY$
                SELECT 
                    cs.""CompaniesId"",
                    cs.""Name"",
                    cs.""TradeName"",
                    cs.""TaxNumber"",
                    ci.""CEP"",
                    ci.""Street"",
                    ci.""PropertyNumber"",
                    ci.""District"",
                    ci.""City"",
                    ci.""State"",
                    ci.""Country"",
                    ci.""AdressComplement"",
                    ci.""PhoneNumber"",
                    ci.""Email"",
                    ci.""Observations"",
                    ci.""CompanyStatus"",
                    ci.""CompanyInformationId""
                FROM ""Companies"" AS cs
                INNER JOIN ""CompanyInformation"" AS ci
                ON cs.""CompanyInformationId"" = ci.""CompanyInformationId""
                WHERE cs.""TaxNumber"" = ""paramTaxNumber"";
            $BODY$;
            ";

            migrationBuilder.Sql(dropFunctionSql);
            migrationBuilder.Sql(createFunctionSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropFunctionSql = @"
            DROP FUNCTION IF EXISTS public.getcompanybytaxnumber;
            ";

            migrationBuilder.RenameColumn(
                name: "TaxNumber",
                table: "Companies",
                newName: "CNPJ");

            var createFunctionSql = @"
            CREATE OR REPLACE FUNCTION public.getcompanybycnpj(cnpj text)
            RETURNS TABLE(
                ""CompaniesId"" int,
                ""Name"" text,
                ""TradeName"" text,
                ""CNPJ"" text,
                ""CEP"" text,
                ""Street"" text,
                ""PropertyNumber"" text,
                ""District"" text,
                ""City"" text,
                ""State"" text,
                ""Country"" text,
                ""AdressComplement"" text,
                ""PhoneNumber"" text,
                ""Email"" text,
                ""Observations"" text,
                ""CompanyStatus"" int,
                ""CompanyInformationId"" int
            ) 
            LANGUAGE 'sql'
            COST 100
            VOLATILE PARALLEL UNSAFE
            ROWS 1000
            AS $BODY$
                SELECT 
                    cs.""CompaniesId"",
                    cs.""Name"",
                    cs.""TradeName"",
                    cs.""CNPJ"",
                    ci.""CEP"",
                    ci.""Street"",
                    ci.""PropertyNumber"",
                    ci.""District"",
                    ci.""City"",
                    ci.""State"",
                    ci.""Country"",
                    ci.""AdressComplement"",
                    ci.""PhoneNumber"",
                    ci.""Email"",
                    ci.""Observations"",
                    ci.""CompanyStatus"",
                    ci.""CompanyInformationId""
                FROM ""Companies"" AS cs
                INNER JOIN ""CompanyInformation"" AS ci
                ON cs.""CompanyInformationId"" = ci.""CompanyInformationId""
                WHERE cs.""CNPJ"" = cnpj;
            $BODY$;
            ";

            migrationBuilder.Sql(dropFunctionSql);
            migrationBuilder.Sql(createFunctionSql);
        }
    }
}
