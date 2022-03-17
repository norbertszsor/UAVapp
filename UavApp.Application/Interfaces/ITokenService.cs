using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Entities;

namespace UavApp.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
        Task<(string, bool)> GenerateToken(Guid id);
        Task<(string, bool)> ValidateToken(string token);
        int GenerateSerial();

    }
}
