using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{
    public interface ISearchAccounts
    {
        public Task<ICollection<SearchAccountDto>> GetAllAccounts();
    }
}