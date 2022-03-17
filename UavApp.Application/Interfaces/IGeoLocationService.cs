using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.LocationDto;
using UavApp.Domain.Entities;

namespace UavApp.Application.Interfaces
{
    public interface IGeoLocationService
    {
        Task<(string, LocationDto)> GetDroneLocation(int DroneSerial);
        Task<(string, IEnumerable<LocationDto>)> GetUserDronesLocation();
        Task<(string, bool)> SetDroneLocation(LocationDto locationData, int DroneSerial);
    }
}
