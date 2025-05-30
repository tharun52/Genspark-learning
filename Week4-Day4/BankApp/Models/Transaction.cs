namespace BankApp.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public Account FromAccount { get; set; } = null!;
        public Account ToAccount { get; set; } = null!;
    }
}