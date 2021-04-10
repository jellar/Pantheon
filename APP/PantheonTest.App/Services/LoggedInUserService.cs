using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PantheonTest.Application.Contracts;

namespace PantheonTest.App.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; }
    }
}
