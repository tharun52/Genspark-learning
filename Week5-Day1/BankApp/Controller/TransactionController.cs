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
        [HttpGet("sent/{accountId}")]
        public async Task<ActionResult<ICollection<SearchTransactionDto>>> GetSentTransactions(int accountId)
        {
            try
            {
                var transactions = await _transactionService.SearchSentTransactionsByAccountId(accountId);
                if (transactions != null && transactions.Count > 0)
                {
                    return Ok(transactions);
                }
                return NotFound("No sent transactions found for the specified account ID.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("received/{accountId}")]
        public async Task<ActionResult<ICollection<SearchTransactionDto>>> GetReceivedTransactions(int accountId)
        {
            try
            {
                var transactions = await _transactionService.SearchReceivedTransactionsByAccountId(accountId);
                if (transactions != null && transactions.Count > 0)
                {
                    return Ok(transactions);
                }
                return NotFound("No received transactions found for the specified account ID.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("date-range")]
        public async Task<ActionResult<ICollection<SearchTransactionDto>>> GetTransactionsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var transactions = await _transactionService.SearchTransactionsByDateRange(startDate, endDate);
                if (transactions != null && transactions.Count > 0)
                {
                    return Ok(transactions);
                }
                return NotFound("No transactions found for the specified date range.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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