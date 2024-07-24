using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.DTOs.Response
{
       public class GetVehicleDto
    {
        public int Id { get; set; }
        public int VehicleModelId { get; set; }
        public string LicensePlate { get; set; }
        public string Chassis { get; set; }
        public string Color { get; set; }
        public FuelType FuelType { get; set; }
        public int Mileage { get; set; }
        public VehicleStatus Status { get; set; }
        public int? ContractId { get; set; }
        public string ManufactureYear { get; set; }
        public string ModelYear { get; set; }
        public string ModelName { get; set; }
        public VehicleManufacturer Manufacturer { get; set; }
        public string Observations { get; set; }
    }
}