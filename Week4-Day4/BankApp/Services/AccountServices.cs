
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

        public Task<Account> GetAccountById(int accountId)
        {
            throw new NotImplementedException();
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