using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.DroneDto;
using UavApp.Application.Interfaces;

namespace UavApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        private readonly IDroneService _droneService;

        public DroneController(IDroneService droneService)
        {
            _droneService = droneService;
        }

        [SwaggerOperation(Summary = "Retrieves  drones")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            (string info, IEnumerable<DroneViewDto> key) state = await _droneService.GetAllDronesAsync();
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }
        [SwaggerOperation(Summary = "Retrieves user drones")]
        [HttpGet("GetAllUserDrone")]
        public async Task<IActionResult> GetAllUserDrones()
        {
            (string info, IEnumerable<DroneViewDto> key) state = await _droneService.GetAllUserDrones();
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary ="Retrive specific drone by id")]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            (string info, DroneViewDto key) state = await _droneService.GetDroneByGuidAsync(id);
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Retrive specific drone by serial")]
        [HttpGet("GetBySerial")]
        public async Task<IActionResult> GetBySerial(int serial)
        {
            (string info, DroneViewDto key) state = await _droneService.GetDroneBySerialAsync(serial);
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Create drone")]
        [HttpPost]
        public async Task<IActionResult> CreateDrone(DroneCreateDto drone)
        {
            (string info, DroneCreateDto key) state = await _droneService.CreateDroneAsync(drone);
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Asing drone to specific user")]
        [HttpPut("Register")]
        public async Task<IActionResult> RegisterDrone(DroneRegisterDto providedData)
        {
            (string info, bool key) state = await _droneService.RegisterDroneAsync(providedData);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }
        [SwaggerOperation(Summary = "Unregister drone from specific user")]
        [HttpPut("Unregister")]
        public async Task<IActionResult> UnregisterDrone(int serial)
        {
            (string info, bool key) state = await _droneService.UnregisterDroneAsync(serial);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Edit specific drone")]
        [HttpPut("Edit")]
        public async Task<IActionResult> EditDrone(DroneEditDto drone)
        {
            (string info, bool key) state = await _droneService.EditDroneAsync(drone);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Delete specific drone")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDrone(int serial)
        {
            (string info, bool key) state = await _droneService.DeleteDroneAsync(serial);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }
    }
}
