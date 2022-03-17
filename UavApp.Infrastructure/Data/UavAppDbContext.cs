using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Common;
using UavApp.Domain.Entities;

namespace UavApp.Infrastructure.Data
{
    public class UavAppDbContext : DbContext
    {
        public UavAppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get; set;}   
        public DbSet<Drone> Drones {get; set;}
    }
}
