
using BankApp.Models;
using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{
    public interface IAccountService
    {
        public Task<Account> CreateAccount(CreateAccountRequestDto createAccountRequestDto);
        public Task<Account> GetAccountById(int accountId);
        public Task<ICollection<SearchAccountDto>> GetAllAccounts();
    }
}