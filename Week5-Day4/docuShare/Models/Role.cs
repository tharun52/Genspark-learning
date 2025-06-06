using System.ComponentModel.DataAnnotations;

namespace docuShare.Models
{
    public class Role
    {
        [Key]
        public string Name { get; set; } = string.Empty;
        public int AccessLevel { get; set; } = 0; 
    }
}