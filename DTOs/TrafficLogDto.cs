using System;

namespace VehicleTrafficManagement.Dto
{
    public class TrafficLogDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public int DriverId { get; set; }
        public int Mileage { get; set; }
    }
}
