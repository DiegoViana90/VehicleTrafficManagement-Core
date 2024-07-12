using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class InsertVehicleModelRequestDto
    {
        public string ModelName { get; set; }
        public string Manufacturer { get; set; }
        public string? Observations { get; set; }
    }
}