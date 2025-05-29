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
    }
}