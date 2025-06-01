using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Models.DTOs;

namespace BankApp.SearchFunctions
{
    public class SearchAccounts : ISearchAccounts
    {
        private readonly BankContext _bankContext;

        public SearchAccounts(BankContext context)
        {
            _bankContext = context;
        }
        public Task<ICollection<SearchAccountDto>> GetAllAccounts()
        {
            try
            {
                return _bankContext.GetAllAccounts();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving accounts.", ex);
            }
        }

        public Task<SearchAccountDto?> GetAccountById(int accountId)
        {
            try
            {
                return _bankContext.GetAccountById(accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the account with ID {accountId}.", ex);
            }
        }
        public Task<ICollection<SearchAccountDto>> GetAccountByName(string accountHolderName)
        {
            try
            {
                return _bankContext.GetAccountByName(accountHolderName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving accounts with the name {accountHolderName}.", ex);
            }
        }
        public Task<ICollection<SearchAccountDto>> GetAccountByEmail(string email)
        {
            try
            {
                return _bankContext.GetAccountByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving accounts with the email {email}.", ex);
            }
        }
    }
}