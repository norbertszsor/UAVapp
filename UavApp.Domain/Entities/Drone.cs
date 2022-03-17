using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Domain.Common;

namespace UavApp.Domain.Entities
{
    public class Drone : AuditableEntity
    {
        public Guid Id { get; set; }
        public string CustomName { get; set; }
        public string Model { get; set; }
        public int Serial { get; set; }       
        public bool IsActive { get; set; }
        public string DroneData { get; set; }
        public virtual User User { get; set; }
    }
}
