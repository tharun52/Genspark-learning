
using BankApp.Contexts;
using BankApp.Models.DTOs;

namespace BankApp.SearchFunctions
{
    public class SearchTransactions
    {
        private readonly BankContext _bankContext;
        public SearchTransactions(BankContext context)
        {
            _bankContext = context;
        }
        public Task<ICollection<SearchTransactionDto>> SearchSentTransactionsByAccountId(int accountId)
        {
            try
            {
                return _bankContext.GetSentTransactionsByAccountId(accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving sent transactions for account ID {accountId}.", ex);
            }
        }
        public Task<ICollection<SearchTransactionDto>> SearchReceivedTransactionsByAccountId(int accountId)
        {
            try
            {
                return _bankContext.GetRecievedTransactionsByAccountId(accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving received transactions for account ID {accountId}.", ex);
            }
        }
        public Task<ICollection<SearchTransactionDto>> SearchTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _bankContext.GetTransactionsByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving transactions between {startDate} and {endDate}.", ex);
            }
        }
    }
}