using System;
using System.Collections.Generic;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.DTOs
{
    public class ContractDto
    {
        public int Id { get; set; }
        public int ServiceProviderCompanyId { get; set; }
        public int ClientCompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContractStatus Status { get; set; }
        public List<int> VehicleIds { get; set; }
    }
}
