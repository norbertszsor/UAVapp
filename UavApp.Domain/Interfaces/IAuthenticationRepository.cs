using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Entities;

namespace UavApp.Domain.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<User> GetUserAsync(string email);
    }
}
