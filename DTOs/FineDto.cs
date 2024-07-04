using System;

namespace VehicleTrafficManagement.Dto
{
    public class FineDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
