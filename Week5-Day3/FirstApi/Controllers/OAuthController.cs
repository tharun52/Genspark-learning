using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Services;

namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OAuthController : ControllerBase
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly ITokenService _tokenService;

        public OAuthController(IRepository<string, User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpGet("login-google")]
        public IActionResult LoginWithGoogle()
        {
            // var redirectUrl = Url.Action("GoogleResponse", "OAuth");
            // var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            // return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, GoogleDefaults.AuthenticationScheme);
        }

        // [HttpGet("google-response")]
        // public async Task<IActionResult> GoogleResponse()
        // {
        //     var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //     if (!result.Succeeded)
        //     {
        //         return Unauthorized("OAuth Authentication failed");
        //     }

        //     var email = result.Principal.FindFirst("email")?.Value;
        //     var name = result.Principal.FindFirst("name")?.Value;

        //     if (string.IsNullOrEmpty(email))
        //     {
        //         return BadRequest("Email not found in external provider.");
        //     }

        //     // Check if user exists
        //     var user = await _userRepository.Get(email);
        //     if (user == null)
        //     {
        //         user = new User
        //         {
        //             Username = email,
        //             Role = "Doctor",
        //             Password = null,
        //             HashKey = null
        //         };
        //         await _userRepository.Add(user);
        //     }

        //     var token = _tokenService.GenerateToken(user);

        //     return Ok(new { token });
        // }
        
        [HttpGet("google-login-callback")]
        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return Unauthorized("Google authentication failed.");

            var claimsPrincipal = authenticateResult.Principal;

            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? 
                        claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _userRepository.Get(email);
            if (user == null)
            {
                user = new User
                {
                    Username = email,
                    Role = "Doctor", 
                };

                await _userRepository.Add(user);
            }

            var jwtToken = await _tokenService.GenerateToken(user);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { token = jwtToken });
        }


    }
}