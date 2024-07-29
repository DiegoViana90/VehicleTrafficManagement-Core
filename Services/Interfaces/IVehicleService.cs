using System.Threading.Tasks;
using VehicleTrafficManagement.DTOs.Request;
using VehicleTrafficManagement.DTOs.Response;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IVehicleService
    {
        Task<NewVehicleResponseDTO> InsertVehicle(InsertVehicleRequestDto insertVehicleRequestDto);
        Task<IEnumerable<VehicleModelDtoResponse>> GetAllVehicleModel();
        Task<IEnumerable<GetVehicleDto>> GetAllVehicles();
        Task<GetVehicleDto> GetVehicleById(int id);
        Task<GetVehicleDto> GetVehicleModelById(int id);
        Task<GetVehicleDto> GetVehicleByChassis(string chassis, int companyId);
        Task<GetVehicleDto> GetVehicleByLicensePlate(string licensePlate, int companyId);
        Task InsertVehicleModel(InsertVehicleModelRequestDto insertVehicleModelRequestDto);
        Task<GetVehicleDto> GetVehicleByQRCode(string QRCode, int companyId);   
        Task<IEnumerable<GetVehicleDto>> GetAllVehiclesFromCompany(int companyId);
    }
}
