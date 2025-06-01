using BankApp.Models;

namespace BankApp.Interfaces
{
    public interface IFaqRepository
    {
        Task<List<Faq>> GetFaqsAsync();
    }
}