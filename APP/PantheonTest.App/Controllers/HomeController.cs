using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PantheonTest.Application.Contracts;
using PantheonTest.Application.Features.Account.Queries;

namespace PantheonTest.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMediator mediator, ILoggedInUserService loggedInUserService, ILogger<HomeController> logger)
        {
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var id = Guid.Parse(_loggedInUserService.UserId);
                var account = await _mediator.Send(new GetAccountDetailsQuery()  { UserId = id});
                return Ok(account);
            }
            catch (Exception e)
            {
                _logger.LogError("Get account failed: " + e.Message);
                throw;
            }
           
        }
    }
}
