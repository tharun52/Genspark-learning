using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{
    public interface ISearchTransactions
    {
        public Task<ICollection<SearchTransactionDto>> SearchSentTransactionsByAccountId(int accountId);
        public Task<ICollection<SearchTransactionDto>> SearchReceivedTransactionsByAccountId(int accountId);
        public Task<ICollection<SearchTransactionDto>> SearchTransactionsByDateRange(DateTime startDate, DateTime endDate);
    }
}