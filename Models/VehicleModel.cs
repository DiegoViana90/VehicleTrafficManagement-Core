using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.Models
{
    public class VehicleModel
    {        
        public int VehicleModelId { get; set; }
        public string ModelName { get; set; }
        public string Manufacturer { get; set; }
        public string? Observations { get; set; }
    }
}