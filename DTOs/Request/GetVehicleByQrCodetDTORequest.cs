using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class GetVehicleByQrCodetDTORequest
    {
        public string QRCode { get; set; }       
    }
}