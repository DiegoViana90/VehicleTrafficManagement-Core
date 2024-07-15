using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Response
{
    public class NewVehicleResponseDTO
    {
        public string Message { get; set; }
        public string LicensePlate { get; set; }
        public string StringQRCODE { get; set; }
    }
}