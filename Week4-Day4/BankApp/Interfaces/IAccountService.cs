
using BankApp.Models;
using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{
    public interface IAccountService
    {
        public Task<Account> CreateAccount(CreateAccountRequestDto createAccountRequestDto);
        public Task<SearchAccountDto?> GetAccountById(int accountId);
        public Task<ICollection<SearchAccountDto>> GetAccountByName(string accountHolderName);
        public Task<ICollection<SearchAccountDto>> GetAccountByEmail(string email);
        public Task<ICollection<SearchAccountDto>> GetAllAccounts();
    }
}