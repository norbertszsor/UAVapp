using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.DroneDto;
using UavApp.Application.Interfaces;
using UavApp.Application.Interfaces.ContextInterfaces;
using UavApp.Domain.Common.JsonData;
using UavApp.Domain.Entities;
using UavApp.Domain.Interfaces;

namespace UavApp.Application.Services
{
    public class DroneService : IDroneService
    {
        private readonly IDroneRepository _droneRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;

        public DroneService(IDroneRepository droneRepository, ITokenService tokenService, IMapper mapper, IUserRepository userRepository, IUserContextService userContextService)
        {
            _droneRepository = droneRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _userRepository = userRepository;
            _userContextService = userContextService;
        }

        public async Task<(string, IEnumerable<DroneViewDto>)> GetAllDronesAsync()
        {
            var drones = await _droneRepository.GetAllAsync();
            if (drones.Any() != true) return ("Any drones has been created yet", null);

            return ("", _mapper.Map<IEnumerable<DroneViewDto>>(drones));
        }
        public async Task<(string, IEnumerable<DroneViewDto>)> GetAllUserDrones()
        {
            try
            {
                var userIdCheck = (Guid)_userContextService.GetUserId;
            }
            catch
            {
                return ("You don't have permission to perform this action", null);
            }

            var drones = await _droneRepository.GetDronesAsync((Guid)_userContextService.GetUserId);
            
            return ("", _mapper.Map<IEnumerable<DroneViewDto>>(drones));

        }
        public async Task<(string, DroneViewDto)> GetDroneByGuidAsync(Guid id)
        {
            var drone = await _droneRepository.GetByGuidAsync(id);
            if (drone == null) return ("this drone doesn't exist", null);

            return ("", _mapper.Map<DroneViewDto>(drone));
        }
        public async Task<(string, DroneViewDto)> GetDroneBySerialAsync(int serialNumber)
        {
            var drone = await _droneRepository.GetBySerialAsync(serialNumber);
            if (drone == null) return ("this drone doesn't exist", null);

            return ("", _mapper.Map<DroneViewDto>(drone));
        }
        public async Task<(string, DroneCreateDto)> CreateDroneAsync(DroneCreateDto providedDrone)
        {
            var drone = new Drone()
            {
                Model = providedDrone.Model,
                IsActive = false,
                Serial = _tokenService.GenerateSerial(),
                Created = DateTime.UtcNow,
                CreatedBy = "SYSTEM"
            };

            return await _droneRepository.CreateAsync(drone) == true ? ("", providedDrone) : ("error", null);
        }
        public async Task<(string, bool)> EditDroneAsync(DroneEditDto providedDrone)
        {
            var drone = await _droneRepository.GetBySerialAsync(providedDrone.Serial);
            if (drone == null) return ("This drone doesn't exist", false);

            drone.LastModified = DateTime.UtcNow;
            drone.LastModifiedBy = "SYSTEM";
            drone.CustomName = providedDrone.CustomName;
            drone.IsActive = providedDrone.IsActive;

            return await _droneRepository.UpdateAsync(drone) == true ? ("success", true) : ("error", false);
        }
        public async Task<(string, bool)> RegisterDroneAsync(DroneRegisterDto providedDrone)
        {
            try
            {
                var userIdCheck = (Guid)_userContextService.GetUserId;
            }
            catch
            {
                return ("You don't have permission to perform this action", false);
            }

            var userId = (Guid)_userContextService.GetUserId;
            var user = await _userRepository.GetByGuidAsync(userId);

            var drone = await _droneRepository.GetBySerialAsync(providedDrone.Serial);
            if (drone == null) return ("Unvalid serial number",false);

            if (user.Drones == null)
            {
                user.Drones = new List<Drone>();
            }
            else
            {
                if (user.Drones.Contains(drone))
                    return ("This drone has been alredy registred", false);
            }
            
            drone.User = user;
            drone.IsActive = true;
            drone.CustomName = providedDrone.CustomName;
            
            await _droneRepository.UpdateAsync(drone);
            
            user.Drones = user.Drones.Append(drone).ToList();

            return await _userRepository.UpdateAsync(user) == true ? ("success",true) : ("error",false);

        }
        public async Task<(string, bool)> UnregisterDroneAsync(int serialNumber)
        {
            try
            {
                var userIdCheck = (Guid)_userContextService.GetUserId;
            }
            catch
            {
                return ("You don't have permission to perform this action", false);
            }

            var userId = (Guid)_userContextService.GetUserId;
            var user = await _userRepository.GetByGuidAsync(userId);

            var drone = await _droneRepository.GetBySerialAsync(serialNumber);
            if (drone == null) return ("Unvalid serial number", false);

            user.Drones = user.Drones.Where(d => d.Serial != serialNumber);

            return await _userRepository.UpdateAsync(user) == true ? ("success", true) : ("error", false);
        }
        public async Task<(string, bool)> DeleteDroneAsync(int serial)
        {
            var drone = await _droneRepository.GetBySerialAsync(serial);
            
            if (drone == null) return ("error", false);

            return await _droneRepository.DeleteAsync(drone) == true ? ("success",true) : ("success", false);
        }

    }
}
