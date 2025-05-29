using Microsoft.AspNetCore.Mvc;
using BankApp.Interfaces;
using BankApp.Models.DTOs;
using BankApp.Models;

namespace BankApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<Models.Transaction>> CreateTransaction([FromBody] CreateTransactionRequestDto createTransactionRequestDto)
        {
            try
            {
                var transaction = await _transactionService.CreateTransaction(createTransactionRequestDto);
                if (transaction != null)
                {
                    return Created("", transaction);
                }
                return BadRequest("Transaction creation failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}