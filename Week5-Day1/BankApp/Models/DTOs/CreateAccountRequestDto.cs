namespace BankApp.Models.DTOs
{
    public class CreateAccountRequestDto
    {
        public string AccountHolderName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal InitialAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}