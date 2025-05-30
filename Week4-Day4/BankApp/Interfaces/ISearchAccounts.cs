using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{
    public interface ISearchAccounts
    {
        public Task<ICollection<SearchAccountDto>> GetAllAccounts();
        public Task<SearchAccountDto?> GetAccountById(int accountId);
        public Task<ICollection<SearchAccountDto>> GetAccountByName(string accountHolderName);
        public Task<ICollection<SearchAccountDto>> GetAccountByEmail(string email);
    }
}