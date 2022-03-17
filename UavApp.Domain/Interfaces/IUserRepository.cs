using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Entities;

namespace UavApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByGuidAsync(Guid id);
        Task<User> GetByUserEmialAsync(string email);
        Task<bool> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);       
        Task<bool> DeleteAsync(User user);

    }
}
