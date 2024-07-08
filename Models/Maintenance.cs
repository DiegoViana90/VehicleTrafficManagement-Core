using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleTrafficManagement.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public string Observations { get; set; }
    }
}
