namespace FirstApi.Models.DTOs
{
    public class AppointmentAddRequestDto
    {
        public string AppointmentNumber { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; } = DateTime.Now;
    }
}