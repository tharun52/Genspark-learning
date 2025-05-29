
using System.Transactions;
using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Mappers;
using BankApp.Models.DTOs;
namespace BankApp.Services
{
    public class TransactionServices : ITransactionService
    {
        private readonly TransactionMapper _transactionMapper;
        private readonly BankContext _bankContext;

        public TransactionServices(BankContext bankContext, TransactionMapper transactionMapper)
        {
            _transactionMapper = transactionMapper;
            _bankContext = bankContext;
        }

        public async Task<Models.Transaction> CreateTransaction(CreateTransactionRequestDto createTransactionRequestDto)
        {
            using var postgre_transaction = await _bankContext.Database.BeginTransactionAsync();
            var newtransaction = _transactionMapper.MapCreateTransactionRequestDtoToTransaction(createTransactionRequestDto);

            var amount = createTransactionRequestDto.Amount;
            System.Console.WriteLine($"Transaction amount: {amount}");

            if (amount <= 0)
            {
                throw new ArgumentException("Transaction amount must be greater than zero.");
            }

            try
            {
                var fromAccount = await _bankContext.Accounts.FindAsync(createTransactionRequestDto.FromAccountId);
                System.Console.WriteLine($"From Account Balance: {fromAccount.Balance}");
                var toAccount = await _bankContext.Accounts.FindAsync(createTransactionRequestDto.ToAccountId);
                System.Console.WriteLine($"To Account Balance: {toAccount.Balance}");
                if (fromAccount == null || toAccount == null)
                {
                    throw new Exception("Invalid account.");
                }

                if (fromAccount.Balance < amount)
                {
                    throw new Exception("Insufficient funds.");
                }
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;

                await _bankContext.Transactions.AddAsync(newtransaction);
                _bankContext.Accounts.Update(fromAccount);
                _bankContext.Accounts.Update(toAccount);
                await _bankContext.SaveChangesAsync();

                await postgre_transaction.CommitAsync();
                return newtransaction;
            }
            catch (Exception)
            {
                await postgre_transaction.RollbackAsync();
                throw;
            }
        }

        public Task<ICollection<Transaction>> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetTransactionById(int transactionId)
        {
            throw new NotImplementedException();
        }
    }
}