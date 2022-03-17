using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using UavApp.Application.Interfaces;

namespace UavApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        [SwaggerOperation(Summary = "Generate user acivation token")]
        [HttpPut("ResendToken")]
        public async Task<IActionResult> GenerateToken(Guid id)
        {
            (string info, bool key) state = await _tokenService.GenerateToken(id);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Validate user activation token")]
        [HttpPut("ValidateToken")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            (string info, bool key) state = await _tokenService.ValidateToken(token);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }
    }
}
