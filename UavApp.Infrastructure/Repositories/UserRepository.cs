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
    public class UserRepository : IUserRepository
    {
        private readonly UavAppDbContext _dbContext;

        public UserRepository(UavAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
     
        public async Task<User> GetByGuidAsync(Guid id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User> GetByUserEmialAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _dbContext.Attach(user).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(User user)
        {
            _dbContext.Remove(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        
    }
}
