using FirstApi.Models;
using FirstApi.Interfaces;
using FirstApi.Models.DTOs;
using FirstApi.Misc;

namespace FirstApi.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<string, Appointment> _appointmentRepository;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        AppointmentMapper _appointmentMapper;

        public AppointmentService(IRepository<string, Appointment> appointmentRepository,
                                  IDoctorService doctorService,
                                  IPatientService patientService,
                                  AppointmentMapper appointmentMapper)
        {
            _appointmentRepository = appointmentRepository;
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentMapper = appointmentMapper;
        }


        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await _appointmentRepository.GetAll() ?? throw new Exception("No appointments found");
        }



        public async Task<Appointment> SoftDeleteAppointment(string appointmentNumber)
        {
            var appointment = await _appointmentRepository.Get(appointmentNumber) ?? throw new Exception("Appointment not found");
            appointment.Status = $"Deleted by Doctor {appointment.DoctorId}";
            return await _appointmentRepository.Update(appointmentNumber, appointment);
        }


        public async Task<Appointment> GetAppointmentByNumber(string appointmentNumber)
        {
            var appointment = await _appointmentRepository.Get(appointmentNumber) ?? throw new Exception("Appointment not found");
            return appointment;
        }
        

        
        public async Task<Appointment> AddAppointment(AppointmentAddRequestDto appointmentAddRequestDto)
        {
            var doctors = await _doctorService.GetAllDoctors();
            var patients = await _patientService.GetAllPatients();
            if (!doctors.Any(d => d.Id == appointmentAddRequestDto.DoctorId))
            {
                throw new Exception("Doctor is not found in database");
            }
            if (!patients.Any(p => p.Id == appointmentAddRequestDto.PatientId))
            {
                throw new Exception("Patient is not found in the database");
            }
            if (appointmentAddRequestDto.AppointmentDateTime < DateTime.Now.AddHours(1))
            {
                throw new Exception("Appointment date cannot be in the past");
            }
            var newAppointment = _appointmentMapper.MapAppointmentAddRequestAppointment(appointmentAddRequestDto) ?? throw new Exception("Error while Mapping Appoitment");
            newAppointment.Status = "Active";
            newAppointment = await _appointmentRepository.Add(newAppointment);
            return newAppointment;
        }
    }
}