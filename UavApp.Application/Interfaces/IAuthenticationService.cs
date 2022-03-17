using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects;

namespace UavApp.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(string, bool)> GenerateJwtAsync(AuthenticationDto unknownUser);
    }
}
