using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.UserDto;
using UavApp.Application.Interfaces;
using UavApp.Application.Interfaces.ContextInterfaces;
using UavApp.Domain.Common.JsonData;
using UavApp.Domain.Entities;
using UavApp.Domain.Interfaces;

namespace UavApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<UserData> _passwordHasher;

        public UserService(IUserRepository userRepository, IUserContextService userContextService, 
            ITokenService tokenService, IMapper mapper, IPasswordHasher<UserData> passwordHasher)
        {
            _userRepository = userRepository;
            _userContextService = userContextService;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<(string, IEnumerable<UserViewDto>)> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if(users.Any()!=true) return ("Any user has been created yet", null);

            return ("", _mapper.Map<IEnumerable<UserViewDto>>(users));
        }

        public async Task<(string, UserViewDto)> GetUserByGuidAsync(Guid id)
        {
            var user = await _userRepository.GetByGuidAsync(id);
            if (user == null) return ("this user doesn't exist", null);

            return ("", _mapper.Map<UserViewDto>(user));
        }

        public async Task<(string, UserDetailsDto)> GetDetailsAsync()
        {
            try
            {
                var userIdCheck = (Guid)_userContextService.GetUserId;
            }
            catch
            {
                return ("You don't have permission to perform this action", null);
            }

            var userId = (Guid)_userContextService.GetUserId;
            var user = await _userRepository.GetByGuidAsync(userId);

            return ("", _mapper.Map<UserDetailsDto>(user));
        }

        public async Task<(string, bool)> RegisterUserAsync(UserRegisterDto providedUser)
        {
            UserData userData = new UserData();
            userData.password.passwordHash = _passwordHasher.HashPassword(userData, providedUser.Password);
            userData.password.toReset = false;
            string jsonUserData = DataService.SetUserData(userData);

            var user = new User()
            {
                Email = providedUser.Email,
                UserData = jsonUserData,
                Name = providedUser.Name,
                IsActive = false,
                IsAdmin = false,
                Created = DateTime.UtcNow,
                CreatedBy = "SYSTEM"
            };

            if(await _userRepository.CreateAsync(user) != true) return ("User create action failed", false);
           
            var token = await _tokenService.GenerateToken(user);


            return (AppAuthSettings.AppAdress+"/api/User/Active?token="+token+"&userEmail="+user.Email, true);
        }
        public async Task<(string, bool)> ActivateUserAsync(string token, string userEmail)
        {
            var state = await _tokenService.ValidateToken(token);
            var user = await _userRepository.GetByUserEmialAsync(userEmail);

            if (!state.Item2) return (state.Item1, false);
            if (user == null) return ("Inavelid user", false);

            user.IsActive = true;
            return await _userRepository.UpdateAsync(user) == true ? ("success", true) : ("fail", false);

        }

        public async Task<(string,bool)> ChangePasswordAsync(UserPasswordChangeDto providedPassData, string resetToken) 
        {
            //do zrobienia
            try
            {
                var userIdCheck = (Guid)_userContextService.GetUserId;
            }
            catch
            {
                return ("You don't have permmision to perform this action", false);
            }

            var userId = (Guid)_userContextService.GetUserId;
            var user = await _userRepository.GetByGuidAsync(userId);

            UserData userData = DataService.GetUserData(user);

            var result = _passwordHasher.VerifyHashedPassword(userData, userData.password.passwordHash, providedPassData.OldPassword);

            if (result == PasswordVerificationResult.Failed) return ("Wrong Old Password", false);
            if (providedPassData.NewPassword != providedPassData.ConfirmNewPassword) return ("Passwords doesn't match", false);

            userData.password.passwordHash = _passwordHasher.HashPassword(userData, providedPassData.NewPassword);

            user.UserData = DataService.SetUserData(userData);
            return await _userRepository.UpdateAsync(user) == true ? ("Change password action was successful", true) : ("Change password action failed", false);
        }

        public async Task<(string, bool)> EditUserAsync(UserEditDto providedUser)
        {
            try
            {
                var userIdCheck = (Guid)_userContextService.GetUserId;
            }
            catch
            {
                return ("You don't have permmision to perform this action", false);
            }

            var userId = (Guid)_userContextService.GetUserId;
            var user = await _userRepository.GetByGuidAsync(userId);

            user.Name = providedUser.Name;
            user.Email = providedUser.Email;

            return await _userRepository.UpdateAsync(user) == true ? ("success", true) : ("error", false);
        }

        public async Task<(string, bool)> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByGuidAsync(id);
            if (user == null) return ("Invalid user", false);
            return await _userRepository.DeleteAsync(user) == true ? ("Delete user action was successful", true) : ("Delete user action failed", false);
        }
    }
}
