using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UavApp.Domain.Common.JsonData
{
    public class UserData
    {
        public (string passwordHash, bool toReset) password;
        public string token;
    }
}
