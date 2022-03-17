using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Entities;

namespace UavApp.Domain.Interfaces
{
    public interface IDroneRepository
    {
        Task<IEnumerable<Drone>> GetAllAsync();
        Task<Drone> GetByGuidAsync(Guid id);
        Task<IEnumerable<Drone>> GetDronesAsync(Guid userId);
        Task<Drone> GetBySerialAsync(int Serial);
        Task<bool> CreateAsync(Drone drone);
        Task<bool> UpdateAsync(Drone drone);
        Task<bool> DeleteAsync(Drone drone);

    }
}
