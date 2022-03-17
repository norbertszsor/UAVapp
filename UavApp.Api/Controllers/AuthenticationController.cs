using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects;
using UavApp.Application.Interfaces;

namespace UavApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [SwaggerOperation(Summary = "Generate authentication token")]
        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationDto providedUser)
        {
            (string info, bool key) state = await _authenticationService.GenerateJwtAsync(providedUser);
            return state.key == true ? Ok(state.info) : Unauthorized(state.info);
        }
    }
}
