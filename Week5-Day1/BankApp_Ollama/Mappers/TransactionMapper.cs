
using BankApp.Models.DTOs;

namespace BankApp.Mappers
{
    public class TransactionMapper
    {
        public Models.Transaction MapCreateTransactionRequestDtoToTransaction(CreateTransactionRequestDto createTransactionRequestDto)
        {
            if (createTransactionRequestDto == null)
            {
                throw new ArgumentNullException(nameof(createTransactionRequestDto), "CreateTransactionRequestDto cannot be null");
            }

            Models.Transaction transaction = new();
            transaction.FromAccountId = createTransactionRequestDto.FromAccountId;
            transaction.ToAccountId = createTransactionRequestDto.ToAccountId;
            transaction.Amount = createTransactionRequestDto.Amount;
            transaction.TransactionDate = createTransactionRequestDto.TransactionDate; 
            return transaction;
        }
    }
}