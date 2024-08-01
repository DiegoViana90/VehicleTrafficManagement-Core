namespace VehicleTrafficManagement.DTOs.Request
{
    public class GetVehicleHistoricRequest
    {
        public int? VehicleId { get; set; }
        public string? Chassi { get; set; }
        public string? LicensePlate { get; set; }
    }
}
