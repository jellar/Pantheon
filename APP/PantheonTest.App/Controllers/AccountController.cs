using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PantheonTest.Application.Contracts.Identity;
using PantheonTest.Application.Models.Authentication;

namespace PantheonTest.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthenticationService authenticationService, ILogger<AccountController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            try
            {
                var result = await _authenticationService.AuthenticateAsync(request);
                return Ok(result);
            }
            catch
            {
                _logger.LogError("Login failed");
                return BadRequest();
            }
        }
    }
}
