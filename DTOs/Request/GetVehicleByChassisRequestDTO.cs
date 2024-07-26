using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class GetVehicleByLicensePlateRequestDTO
    {
        public string LicensePlate { get; set; }  
        public int CompaniesId { get; set; }     
    }
}