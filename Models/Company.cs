using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleTrafficManagement.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string CNPJ { get; set; }
        public string Observations { get; set; }
        public bool IsOpen { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
