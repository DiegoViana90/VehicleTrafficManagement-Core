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

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Observations { get; set; }
    }
}
