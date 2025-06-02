using BankApp.Models;

namespace BankApp.Interfaces
{
    public interface IFAQRepository
    {
        IEnumerable<Faq> GetAll();
    }
}