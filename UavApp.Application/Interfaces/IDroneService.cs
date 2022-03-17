using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.DroneDto;

namespace UavApp.Application.Interfaces
{
    public interface IDroneService
    {
        Task<(string, IEnumerable<DroneViewDto>)> GetAllDronesAsync();
        Task<(string, IEnumerable<DroneViewDto>)> GetAllUserDrones();
        Task<(string, DroneViewDto)> GetDroneByGuidAsync(Guid id);
        Task<(string, DroneViewDto)> GetDroneBySerialAsync(int serialNumber);
        Task<(string, DroneCreateDto)> CreateDroneAsync(DroneCreateDto providedDrone);
        Task<(string, bool)> RegisterDroneAsync(DroneRegisterDto providedDrone);
        Task<(string, bool)> UnregisterDroneAsync(int serialNumber);
        Task<(string, bool)> EditDroneAsync(DroneEditDto providedDrone);
        Task<(string, bool)> DeleteDroneAsync(int serial);
    }
}
