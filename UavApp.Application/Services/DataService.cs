using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.Interfaces;
using UavApp.Domain.Common.JsonData;
using UavApp.Domain.Entities;
using UavApp.Domain.Interfaces;

namespace UavApp.Application.Services
{
    public static class DataService
    {
        public static UserData GetUserData(User user)
        {
            UserData tmpUserData = JsonConvert.DeserializeObject<UserData>(user.UserData);
            return tmpUserData;
        }
        public static string SetUserData(UserData userData)
        {
            string jsonUserData = JsonConvert.SerializeObject(userData,Formatting.Indented);
            return jsonUserData;
        }

        public static DroneData GetDroneData(Drone drone)
        {
            DroneData tmpDroneData = JsonConvert.DeserializeObject<DroneData>(drone.DroneData);
            return tmpDroneData;
        }

        public static string SetDroneData(DroneData droneData)
        {
            string jsonDroneData = JsonConvert.SerializeObject(droneData, Formatting.Indented);
            return jsonDroneData;
        }
    }
}
