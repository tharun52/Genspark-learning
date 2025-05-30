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
        [HttpGet("search/Id/{accountId}")]
        public async Task<ActionResult<SearchAccountDto>> GetAccountById(int accountId)
        {
            try
            {
                var account = await _accountService.GetAccountById(accountId);
                if (account == null)
                {
                    return NotFound($"Account with ID {accountId} not found.");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search/name/{accountHolderName}")]
        public async Task<ActionResult<IEnumerable<SearchAccountDto>>> GetAccountByName(string accountHolderName)
        {
            try
            {
                var accounts = await _accountService.GetAccountByName(accountHolderName);
                if (accounts == null || !accounts.Any())
                {
                    return NotFound($"No accounts found for name {accountHolderName}.");
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search/email/{email}")]
        public async Task<ActionResult<IEnumerable<SearchAccountDto>>> GetAccountByEmail(string email)
        {
            try
            {
                var accounts = await _accountService.GetAccountByEmail(email);
                if (accounts == null || !accounts.Any())
                {
                    return NotFound($"No accounts found for email {email}.");
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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