using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.Models
{
    public class FuelLog
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public float Liters { get; set; }
        public DateTime Date { get; set; }
        public int Mileage { get; set; }
    }

}