using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Interfaces
{
    public interface IAppointmentService
    {
        public Task<IEnumerable<Appointment>> GetAllAppointments();
        public Task<Appointment> AddAppointment(AppointmentAddRequestDto appointmentAddRequestDto);
        Task<Appointment> GetAppointmentByNumber(string appointmentNumber);
        public Task<Appointment> SoftDeleteAppointment(string appointmentNumber);
    }
}