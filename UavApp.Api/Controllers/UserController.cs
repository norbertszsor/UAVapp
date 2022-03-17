using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.UserDto;
using UavApp.Application.Interfaces;

namespace UavApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Retrieves users")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            (string info, IEnumerable<UserViewDto> key) state = await _userService.GetAllUsersAsync();
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

        //[Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Retrieves specific user")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            (string info, UserViewDto key) state = await _userService.GetUserByGuidAsync(id);
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Register new normal user and create user activation link")]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto newUser)
        {
            (string info, bool key) state = await _userService.RegisterUserAsync(newUser);
            return state.key == true ? Ok(state.info) : Conflict(state.info);
        }
        [SwaggerOperation(Summary = "Activate user via activation link")]
        [HttpPut("Active")]
        public async Task<IActionResult> ActiveUser(string token, string userEmail)
        {
            (string info, bool key) state = await _userService.ActivateUserAsync(token,userEmail);
            return state.key == true ? Ok(state.info) : BadRequest(state.key);
        }

        [SwaggerOperation(Summary = "Retrieves your user account details")]
        [HttpGet("ProflieDetails")]
        public async Task<IActionResult> GetDeails()
        {
            (string info, UserDetailsDto key) state = await _userService.GetDetailsAsync();
            return string.IsNullOrEmpty(state.info) == true ? Ok(state.key) : BadRequest(state.info);
        }

        [SwaggerOperation(Summary = "Change password option for users")]
        [HttpPut("ChangePassword")]

        public async Task<IActionResult> ChangePassword(UserPasswordChangeDto providedPasswordData, string resetToken)
        {
            (string info, bool key) state = await _userService.ChangePasswordAsync(providedPasswordData, resetToken);
            return state.key == true ? Ok(state.info) : BadRequest(state.key);
        }

        [SwaggerOperation(Summary = "Edit specific user")]
        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser(UserEditDto providedData)
        {
            (string info, bool key) state = await _userService.EditUserAsync(providedData);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }

        //[Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Delete specific user")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            (string info, bool key) state = await _userService.DeleteUserAsync(id);
            return state.key == true ? Ok(state.info) : BadRequest(state.info);
        }
    }
}
