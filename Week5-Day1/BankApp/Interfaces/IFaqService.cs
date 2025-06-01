namespace BankApp.Interfaces
{
    public interface IFaqService
    {
        Task<string> GetAnswerAsync(string question);
    }
}