namespace BankApp.Models.DTOs
{
    public class SearchAccountDto
    {
        public int AccountId { get; set; }
        public string AccountHolderName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        asd
    }
}