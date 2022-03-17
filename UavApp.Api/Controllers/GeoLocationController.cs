using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.LocationDto;
using UavApp.Application.Interfaces;

namespace UavApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoLocationController : ControllerBase
    {
        
        private readonly IGeoLocationService _geoLocationService;

        public GeoLocationController(IGeoLocationService geoLocationService)
        {
            this._geoLocationService = geoLocationService;
        }

        [SwaggerOperation(Summary = "Get drone location data")]
        [HttpGet("GetLocation")]
        public async Task<IActionResult> GetDroneLocation(int serial)
        {
            (string info, LocationDto data) state = await _geoLocationService.GetDroneLocation(serial);
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.data) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Get all user drones location data")]
        [HttpGet("GetUserLocations")]
        public async Task<IActionResult> GetUserDronesLocation()
        {
            (string info, IEnumerable<LocationDto> key) state = await _geoLocationService.GetUserDronesLocation();
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

            [SwaggerOperation(Summary = "Set drone location data")]
        [HttpPut]
        public async Task<IActionResult> SetDroneLocation(LocationDto geoData, int serial)
        {
            (string info, bool key) state = await _geoLocationService.SetDroneLocation(geoData, serial);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }

    }
}
