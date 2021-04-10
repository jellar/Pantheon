using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        public HomeController(IMediator mediator, ILoggedInUserService loggedInUserService)
        {
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var id = Guid.Parse(_loggedInUserService.UserId);
            var account = await _mediator.Send(new GetAccountDetailsQuery()  { UserId = id});
            return Ok(account);
        }
    }
}
