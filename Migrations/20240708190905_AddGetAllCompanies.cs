using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTrafficManagement.Migrations
{
    public partial class AddGetAllCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        var sql = @"
        CREATE OR REPLACE FUNCTION public.getallcompanies()
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
        $BODY$;
        ";

        migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        var sql = @"
        DROP FUNCTION IF EXISTS public.getallcompanies;
        ";

        migrationBuilder.Sql(sql);
        }
    }
}
