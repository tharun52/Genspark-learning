namespace BankApp.Models.DTOs
{
    public class CreateTransactionRequestDto
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; } = 0;
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}