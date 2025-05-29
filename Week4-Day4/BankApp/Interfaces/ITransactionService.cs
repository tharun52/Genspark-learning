using System.Transactions;
using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{
    public interface ITransactionService
    {
        public Task<Models.Transaction> CreateTransaction(CreateTransactionRequestDto createTransactionRequestDto);
        public Task<ICollection<Transaction>> GetAllTransactions();
        public Task<Transaction> GetTransactionById(int transactionId);
    }
}