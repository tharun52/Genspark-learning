using System.Transactions;
using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{
    public interface ITransactionService
    {
        public Task<Models.Transaction> CreateTransaction(CreateTransactionRequestDto createTransactionRequestDto);
        public Task<ICollection<SearchTransactionDto>> SearchSentTransactionsByAccountId(int accountId);
        public Task<ICollection<SearchTransactionDto>> SearchReceivedTransactionsByAccountId(int accountId);
        public Task<ICollection<SearchTransactionDto>> SearchTransactionsByDateRange(DateTime startDate, DateTime endDate);
    }
}