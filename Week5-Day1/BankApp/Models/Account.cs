namespace BankApp.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountHolderName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();
        public ICollection<Transaction> RecievedTransactions { get; set; } = new List<Transaction>();

    }
}