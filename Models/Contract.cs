using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Models
{
    public class Contract
    {
        public int Id { get; set; }

        [ForeignKey("ServiceProviderCompany")]
        public int ServiceProviderCompanyId { get; set; }
        public Company ServiceProviderCompany { get; set; }

        [ForeignKey("ClientCompany")]
        public int ClientCompanyId { get; set; }
        public Company ClientCompany { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContractStatus Status { get; set; }
        public bool IsOpen { get; set; }  // Nova propriedade

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
