using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class InsertVehicleRequestDto
    {
        public int VehicleModelId { get; set; }

        public string? LicensePlate { get; set; }
        public string Chassis { get; set; }
        public string Color { get; set; }
        public FuelType FuelType { get; set; }
        public int Mileage { get; set; }
        public VehicleStatus Status { get; set; } = 0;
        public string ManufactureYear { get; set; }
        public string ModelYear { get; set; }
        public int? ContractId { get; set; }
        public int CompaniesId { get; set; }
    }
}
