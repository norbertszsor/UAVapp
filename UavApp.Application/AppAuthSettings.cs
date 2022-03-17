using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UavApp.Application
{
    public static class AppAuthSettings
    {
        public static string JwtKey = "PRIVATE_KEY_DONT_SHARE";
        public static int JwtExpire = 7;
        public static string JwtIssuer = "https://localhost:44308";

        public static string AppAdress = "https://localhost:44308";
        public static string AppTokenSeparator = "default";
    }
}
