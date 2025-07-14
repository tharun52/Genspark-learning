using AzureKeySecret.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureKeySecret.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecretController : ControllerBase
    {
        private readonly IKeyVaultService _keyVaultService;

        public SecretController(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSecret()
        {
            var secretValue = await _keyVaultService.GetSecretValueAsync();
            return Ok(new { Secret = secretValue });
        }
    }
}