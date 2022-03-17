using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Entities;
using UavApp.Domain.Interfaces;
using UavApp.Infrastructure.Data;

namespace UavApp.Infrastructure.Repositories
{
    public class DroneRepository : IDroneRepository
    {
        private readonly UavAppDbContext _dbContext;

        public DroneRepository(UavAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Drone>> GetAllAsync()
        {
            return await _dbContext.Drones.ToListAsync();
        }
        public async Task<IEnumerable<Drone>> GetDronesAsync(Guid userId)
        {
            return await _dbContext.Drones.Where(x => x.User.Id == userId).ToListAsync();
        }

        public async Task<Drone> GetByGuidAsync(Guid id)
        {
            return await _dbContext.Drones.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Drone> GetBySerialAsync(int Serial)
        {
            return await _dbContext.Drones.SingleOrDefaultAsync(x => x.Serial == Serial);
        }

        public async Task<bool> CreateAsync(Drone drone)
        {
            await _dbContext.Drones.AddAsync(drone);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Drone drone)
        {
            _dbContext.Attach(drone).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(Drone drone)
        {
            _dbContext.Remove(drone);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
