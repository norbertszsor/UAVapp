using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.DataTransferObjects;
using UavApp.Application.Interfaces;
using UavApp.Domain.Common.JsonData;
using UavApp.Domain.Interfaces;

namespace UavApp.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepostiory;
        private readonly IPasswordHasher<UserData> _passwordHasher;

        public AuthenticationService(IAuthenticationRepository authenticationRepostiory, IPasswordHasher<UserData> passwordHasher)
        {
            _authenticationRepostiory = authenticationRepostiory;
            _passwordHasher = passwordHasher;
        }

        public async Task<(string, bool)> GenerateJwtAsync(AuthenticationDto unknownUser)
        {
            var user = await _authenticationRepostiory.GetUserAsync(unknownUser.Email);

            if (user == null) return ("Unvalid user or password", false);
            if (!user.IsActive) return ("Unvalid user or password", false);

            UserData tmpUserData = DataService.GetUserData(user);

            var result = _passwordHasher.VerifyHashedPassword(tmpUserData, tmpUserData.password.passwordHash,
                unknownUser.Password);
            if (result == PasswordVerificationResult.Failed) return ("Unvalid user or password", false);

            string abstractRole = user.IsAdmin == true ? "admin" : "user"; 

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, abstractRole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppAuthSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.UtcNow.AddDays(AppAuthSettings.JwtExpire);


            var token = new JwtSecurityToken(AppAuthSettings.JwtIssuer, AppAuthSettings.JwtIssuer, claims, expires: expire, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            return (tokenHandler.WriteToken(token), true);

        }
    }
}

