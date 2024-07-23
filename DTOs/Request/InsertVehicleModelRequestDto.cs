using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class InsertVehicleModelRequestDto
    {
        public string ModelName { get; set; }
        public VehicleManufacturer Manufacturer { get; set; }
        public string? Observations { get; set; }
    }
}