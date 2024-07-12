namespace VehicleTrafficManagement.DTOs.Request
{
    public class InsertVehicleRequestDto
    {
        public string LicensePlate { get; set; }
        public string Chassis { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public string Notes { get; set; }
    }
}
