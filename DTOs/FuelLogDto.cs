using System;

namespace VehicleTrafficManagement.Dto
{
    public class FuelLogDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public decimal Liters { get; set; }
        public DateTime Date { get; set; }
        public int Mileage { get; set; }
    }
}
