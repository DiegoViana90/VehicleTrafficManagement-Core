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
        public string LicensePlate { get; set; }

        public string Chassis { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public string Observations { get; set; }

        public VehicleStatus Status { get; set; }

        [ForeignKey("Contract")]
        public int? ContractId { get; set; }
        public Contract Contract { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }
    }
}
