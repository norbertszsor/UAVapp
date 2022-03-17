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
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;

        public TokenService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> GenerateToken(User user)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] seed = Guid.NewGuid().ToByteArray();
            byte[] key = user.Id.ToByteArray();

            string token = Convert.ToBase64String(time.Concat(seed.Concat(key)).ToArray());

            UserData userData = DataService.GetUserData(user);
            userData.token = token;
            user.UserData = DataService.SetUserData(userData);

            await _userRepository.UpdateAsync(user);

            return token;
        }

        public async Task<(string, bool)> GenerateToken(Guid userId)
        {
            User user = await _userRepository.GetByGuidAsync(userId);

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] seed = Guid.NewGuid().ToByteArray();
            byte[] key = user.Id.ToByteArray();

            string token = Convert.ToBase64String(time.Concat(seed.Concat(key)).ToArray());

            UserData userData = DataService.GetUserData(user);
            userData.token = token;
            user.UserData = DataService.SetUserData(userData);



            if (await _userRepository.UpdateAsync(user) != true) return ("Invalid user", false);

            return (token, true);
        }

        public async Task<(string, bool)> ValidateToken(string token)
        {
            byte[] dataByte = Convert.FromBase64String(token);

            DateTime expireDate = DateTime.FromBinary(BitConverter.ToInt64(dataByte,0));
            if (expireDate < DateTime.UtcNow.AddHours(-24))
            {
                return ("Invalid token or token is expired", false);
            };


            return ("This token is valid",true);
        }

        public int GenerateSerial()
        {
            byte[] key = Guid.NewGuid().ToByteArray();

            int serial = BitConverter.ToInt32(key, 0);

            return serial;
        }
    }
}
