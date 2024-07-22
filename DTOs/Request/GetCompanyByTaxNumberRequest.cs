using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Request
{


    public class GetCompanyByTaxNumberRequest
    {
        public string TaxNumber { get; set; }
    }
}