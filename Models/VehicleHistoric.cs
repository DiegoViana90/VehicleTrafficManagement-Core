using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Models
{
    public class VehicleHistoric
    {
        public int VehicleHistoricId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey("Contract")]
        public int? ContractId { get; set; }
        public Contract? Contract { get; set; }
        public DateTime InclusionDateTime { get; set; }
        public DateTime? RemovalDateTime  { get; set; }
      
    }
}
