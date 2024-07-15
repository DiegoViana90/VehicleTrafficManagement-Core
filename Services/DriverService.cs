using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Services
{
    public class DriverService : IDriverService
    {
        public Task<IEnumerable<DriverDto>> GetAllDrivers()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task<DriverDto> GetDriverById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task InsertDriver(DriverDto driverDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateDriverById(int id, DriverDto driverDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteDriverById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
