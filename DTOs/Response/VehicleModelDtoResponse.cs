using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.DTOs.Response
{
    public class VehicleModelDtoResponse
    {
        public int VehicleModelId { get; set; }
        public string ModelName { get; set; }
        public VehicleManufacturer Manufacturer { get; set; }
        public string? Observations { get; set; }
    }
}
