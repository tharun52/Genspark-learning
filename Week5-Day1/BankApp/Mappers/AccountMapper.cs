using BankApp.Models.DTOs;
using BankApp.Models;

namespace BankApp.Mappers
{
    public class AccountMapper
    {
        public Account MapCreateAccountRequestDtoToAccount(CreateAccountRequestDto createAccountRequestDto)
        {
            Account account = new();
            account.AccountHolderName = createAccountRequestDto.AccountHolderName;
            account.Balance = createAccountRequestDto.InitialAmount;
            account.Email = createAccountRequestDto.Email;
            account.CreatedAt = createAccountRequestDto.CreatedAt;
            return account;
        }
    }
}