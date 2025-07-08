using System.ComponentModel.DataAnnotations;

namespace AzureApp.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}