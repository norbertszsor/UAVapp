using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.LocationDto;
using UavApp.Application.Interfaces;
using UavApp.Application.Interfaces.ContextInterfaces;
using UavApp.Domain.Common.JsonData;
using UavApp.Domain.Entities;
using UavApp.Domain.Interfaces;

namespace UavApp.Application.Services
{
    public class GeoLocationService : IGeoLocationService
    {
        private readonly IDroneRepository _droneRepository;     
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public GeoLocationService(IDroneRepository droneRepository, IUserContextService userContextService, IMapper mapper)
        {
            _droneRepository = droneRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<(string, LocationDto)> GetDroneLocation(int DroneSerial)
        {
            Drone drone = await _droneRepository.GetBySerialAsync(DroneSerial);

            if (drone == null) return ("Location data is empty", null);
            if (!drone.IsActive) return ("Inactive drone", null);

            DroneData droneData = DataService.GetDroneData(drone);
            
            return ("", _mapper.Map<LocationDto>(droneData));

        }

        public async Task<(string, IEnumerable<LocationDto>)> GetUserDronesLocation()
        {
            List<DroneData> dataLocations = new List<DroneData>();

            try
            {
                var userIdCheck = (Guid)_userContextService.GetUserId;
            }
            catch
            {
                return ("You don't have permission to perform this action", null);
            }

            var userId = (Guid)_userContextService.GetUserId;

            var Drones = await _droneRepository.GetDronesAsync(userId);

            foreach (var drone in Drones)
            {
                if (drone.IsActive)
                {
                    dataLocations.Add(DataService.GetDroneData(drone));
                }
                
            }

            if (dataLocations.Count < 0) return ("List of drones is empty", null);

            return ("", _mapper.Map<IEnumerable<LocationDto>>(dataLocations)); 
        }

        public async Task<(string, bool)> SetDroneLocation(LocationDto locationData, int DroneSerial)
        {
            Drone drone = await _droneRepository.GetBySerialAsync(DroneSerial);
            if (drone == null) return ("Unvalid serial number", false);
            if (!drone.IsActive) return ("Inactive drone", false);

            drone.DroneData = DataService.SetDroneData(_mapper.Map<DroneData>(locationData));
            drone.LastModified = DateTime.Now;
            drone.LastModifiedBy = "SYSTEM";

            return await _droneRepository.UpdateAsync(drone) == true ? ("success",true) : ("error",false);
        }
    }
}
