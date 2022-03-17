﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.Interfaces.ContextInterfaces;

namespace UavApp.Application.Services.ContextService
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;
        public Guid? GetUserId => User is null ? null : (Guid?)Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
