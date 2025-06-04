using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Misc
{
    public class AppointmentMapper
    {
        public Appointment? MapAppointmentAddRequestAppointment(AppointmentAddRequestDto appointmentAddRequestDto)
        {
            Appointment appointment = new();
            appointment.AppointmentNumber = appointmentAddRequestDto.AppointmentNumber;
            appointment.DoctorId = appointmentAddRequestDto.DoctorId;
            appointment.PatientId = appointmentAddRequestDto.PatientId;
            appointment.AppointmentDateTime = appointmentAddRequestDto.AppointmentDateTime;
            return appointment;
        }
    }
}