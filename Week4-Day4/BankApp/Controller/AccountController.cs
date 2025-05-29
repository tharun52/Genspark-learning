using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.Interfaces;
using BankApp.Models.DTOs;
using BankApp.Models;

namespace BankApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchAccountDto>>> GetAccounts()
        {
            var result = await _accountService.GetAllAccounts();
            if (result == null || !result.Any())
            {
                return NotFound("No accounts found.");
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] CreateAccountRequestDto createAccountRequestDto)
        {
            try
            {
                var account = await _accountService.CreateAccount(createAccountRequestDto);
                if (account != null)
                {
                    return Created("", account);
                }
                return BadRequest("Account creation failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}