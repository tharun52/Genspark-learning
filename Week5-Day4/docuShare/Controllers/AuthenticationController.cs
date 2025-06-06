using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docuShare.Interfaces;
using docuShare.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace docuShare.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<ActionResult<UserLoginResponse>> UserLogin(UserLoginRequest loginRequest)
        {
            try
             {
                 var result = await _authenticationService.Login(loginRequest);
                 return Ok(result);
             }
             catch (Exception e)
             {
                 return Unauthorized(e.Message);
             }
        }
    }
}