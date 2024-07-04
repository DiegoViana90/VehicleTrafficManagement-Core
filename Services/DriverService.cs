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

        public Task AddDriver(DriverDto driverDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateDriver(int id, DriverDto driverDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteDriver(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
