using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces

{
    public interface IDriverService
    {
        Task<IEnumerable<DriverDto>> GetAllDrivers();
        Task<DriverDto> GetDriverById(int id);
        Task AddDriver(DriverDto driverDto);
        Task UpdateDriver(int id, DriverDto driverDto);
        Task DeleteDriver(int id);
    }
}
