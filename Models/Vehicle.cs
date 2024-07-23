using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("VehicleModel")]
        public int VehicleModelId { get; set; }
        [Required]
        public VehicleModel VehicleModel { get; set; }

        public string? LicensePlate { get; set; }
        public string Chassis { get; set; }
        public string Color { get; set; }
        public FuelType FuelType { get; set; }
        public int Mileage { get; set; }
        public VehicleStatus Status { get; set; } = 0;

        [ForeignKey("Contract")]
        public int? ContractId { get; set; }
        public Contract? Contract { get; set; }
        public ICollection<Maintenance>? Maintenances { get; set; }
        public string StringQRCODE { get; set; }
        public string HashedChassi { get; set; }
        public string ManufactureYear { get; set; }
        public string ModelYear { get; set; }
    }
}
