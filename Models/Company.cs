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

        public CompanyInformation CompanyInformation { get; set; }
    }
}