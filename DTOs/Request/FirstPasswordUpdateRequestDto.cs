using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrafficManagement.DTOs.Request
{
    public class FirstPasswordUpdateRequest
    {
      public int UserId { get; set; }
      public string NewPassword { get; set; }
    }
}