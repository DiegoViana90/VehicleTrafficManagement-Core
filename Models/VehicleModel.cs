using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Models
{
    public class VehicleModel
    {        
        public int VehicleModelId { get; set; }
        public string ModelName { get; set; }
        public VehicleManufacturer Manufacturer { get; set; }
        public string? Observations { get; set; }
    }
}