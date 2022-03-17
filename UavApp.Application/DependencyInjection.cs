using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.Interfaces;
using UavApp.Application.Interfaces.ContextInterfaces;
using UavApp.Application.Services;
using UavApp.Application.Services.ContextService;
using UavApp.Domain.Common.JsonData;


namespace UavApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // add services
            // ...

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = AppAuthSettings.JwtIssuer,
                    ValidAudience = AppAuthSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppAuthSettings.JwtKey)),
                };
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);


            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IGeoLocationService, GeoLocationService>();
            services.AddScoped<IDroneService, DroneService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPasswordHasher<UserData>, PasswordHasher<UserData>>();      
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }


    }
}
