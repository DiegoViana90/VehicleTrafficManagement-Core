namespace VehicleTrafficManagement.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string TaxNumber { get; set; }
        public CompanyInformationDto CompanyInformation { get; set; }
    }
}
