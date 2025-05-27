namespace FirstApi.Models
{
    public class Paitent
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DoctorName { get; set; }
        public int? Age { get; set; }
        public string? Diagnosis { get; set; }
    }
}