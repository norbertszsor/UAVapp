using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UavApp.Application.Interfaces.ContextInterfaces
{
    public interface IUserContextService
    {
        Guid? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}
