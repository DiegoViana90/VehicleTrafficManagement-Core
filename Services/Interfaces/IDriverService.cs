using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces

{
    public interface IDriverService
    {
        Task<IEnumerable<DriverDto>> GetAllDrivers();
        Task<DriverDto> GetDriverById(int id);
        Task InsertDriver(DriverDto driverDto);
        Task UpdateDriverById(int id, DriverDto driverDto);
        Task DeleteDriverById(int id);
    }
}
