using Microsoft.AspNetCore.Mvc;
using MyApiApp.Interfaces;

namespace MyApiApp.Controllers
{
    [ApiController]
    [Route("welcome")]
    public class WelcomeController : ControllerBase
    {
        private readonly IWelcomeService _welcomeService;

        public WelcomeController(IWelcomeService welcomeService)
        {
            _welcomeService = welcomeService;
        }

        [HttpGet]
        public IActionResult GetWelcomeMessage()
        {
            var message = _welcomeService.GetLongWelcomeMessage();
            return Ok(message);
        }
    }
}