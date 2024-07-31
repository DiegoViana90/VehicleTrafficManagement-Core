using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.DTOs.Response
{
       public class GetVehicleHistoricResponse
    {
        public int VehicleId { get; set; }
        public int VehicleHistoricId { get; set; }
        public string LicensePlate { get; set; }
        public string Chassi { get; set; }
        public int? ContractId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyTaxNumber { get; set; }
        public DateTime InclusionDateTime { get; set; }
        public DateTime? RemovalDateTime { get; set; }
    }
}