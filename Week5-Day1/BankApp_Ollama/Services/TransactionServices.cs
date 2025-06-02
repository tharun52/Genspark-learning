
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
                if (fromAccount == null)
                {
                    throw new Exception("Invalid from account.");
                }
                System.Console.WriteLine($"From Account Balance: {fromAccount.Balance}");
                var toAccount = await _bankContext.Accounts.FindAsync(createTransactionRequestDto.ToAccountId);
                if (toAccount == null)
                {
                    throw new Exception("Invalid to account.");
                }
                System.Console.WriteLine($"To Account Balance: {toAccount.Balance}");

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
        public Task<ICollection<SearchTransactionDto>> SearchSentTransactionsByAccountId(int accountId)
        {
            try
            {
                return _bankContext.GetSentTransactionsByAccountId(accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving sent transactions for account ID {accountId}.", ex);
            }
        }
        public Task<ICollection<SearchTransactionDto>> SearchReceivedTransactionsByAccountId(int accountId)
        {
            try
            {
                return _bankContext.GetRecievedTransactionsByAccountId(accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving received transactions for account ID {accountId}.", ex);
            }
        }
        public Task<ICollection<SearchTransactionDto>> SearchTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _bankContext.GetTransactionsByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving transactions between {startDate} and {endDate}.", ex);
            }
        }
    }
}