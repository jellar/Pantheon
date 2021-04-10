﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PantheonTest.Application.Contracts.Identity;
using PantheonTest.Application.Models.Authentication;

namespace PantheonTest.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }
    }
}
