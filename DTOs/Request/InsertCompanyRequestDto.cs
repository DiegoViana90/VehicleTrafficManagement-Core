using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class InsertCompanyRequestDto
    {
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string CNPJ { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public string PropertyNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; } = "Brasil";
        public string AdressComplement { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Observations { get; set; }
        public CompanyStatus CompanyStatus{ get; set; } = 0;
    }
}
