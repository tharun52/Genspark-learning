
using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Mappers;
using BankApp.Models;
using BankApp.Models.DTOs;

namespace BankApp.Services
{
    
    public class AccountServices : IAccountService
    {
        private readonly AccountMapper _accountMapper;
        private readonly ISearchAccounts _searchAccounts;
        private readonly BankContext _bankContext;

        public AccountServices(BankContext bankContext, ISearchAccounts searchAccounts)
        {
            _bankContext = bankContext;
            _accountMapper = new AccountMapper();
            _searchAccounts = searchAccounts;
        }

        public async Task<Account> CreateAccount(CreateAccountRequestDto createAccountRequestDto)
        {
            using var transaction = await _bankContext.Database.BeginTransactionAsync();
            var newAccount = _accountMapper.MapCreateAccountRequestDtoToAccount(createAccountRequestDto);

            try
            {
                await _bankContext.Accounts.AddAsync(newAccount);
                await _bankContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return newAccount;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<SearchAccountDto?> GetAccountById(int accountId)
        {
            try
            {
                var searchResult = _searchAccounts.GetAccountById(accountId);
                if (searchResult == null)
                {
                    throw new Exception($"Account with ID {accountId} not found.");
                }
                return searchResult;
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
                return _searchAccounts.GetAccountByName(accountHolderName);
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
                return _searchAccounts.GetAccountByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving accounts with the email {email}.", ex);
            }
        }
        public Task<ICollection<SearchAccountDto>> GetAllAccounts()
        {
            try
            {
                return _searchAccounts.GetAllAccounts();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving accounts.", ex);
            }
        }
    }
}