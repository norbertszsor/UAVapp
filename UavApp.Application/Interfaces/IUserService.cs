using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects.UserDto;

namespace UavApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<(string, IEnumerable<UserViewDto>)> GetAllUsersAsync();
        Task<(string, UserViewDto)> GetUserByGuidAsync(Guid id);
        Task<(string, UserDetailsDto)> GetDetailsAsync();
        Task<(string, bool)> RegisterUserAsync(UserRegisterDto providedUser);
        Task<(string, bool)> ActivateUserAsync(string token, string userEmail);
        Task<(string, bool)> ChangePasswordAsync(UserPasswordChangeDto providedPassData, string tokenReset);
        Task<(string, bool)> EditUserAsync(UserEditDto providedUser);
        Task<(string, bool)> DeleteUserAsync(Guid id);


    }
}
