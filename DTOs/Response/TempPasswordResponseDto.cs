using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Response
{
    public class TempPasswordResponseDto
    {
        public string Message { get; set; }
        public string RandomPassword { get; set; }
    }
}