using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Common;

namespace UavApp.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserData { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime LastActivity { get; set; }
        public IEnumerable<Drone> Drones { get; set; }
    }
}
