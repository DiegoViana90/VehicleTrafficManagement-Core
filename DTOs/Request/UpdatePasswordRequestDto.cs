using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class UpdateFirstPasswordRequestDto
    {
      public int UserId { get; set; }
      public string RandomPassword { get; set; }
      public string NewPassword { get; set; }
    }
}