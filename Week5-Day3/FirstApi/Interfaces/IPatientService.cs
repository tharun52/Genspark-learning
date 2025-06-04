using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Interfaces
{
    public interface IPatientService
    {
        public Task<Patient> AddPatient(PatientAddRequestDto patientDto);
        public Task<IEnumerable<Patient>> GetAllPatients();
        public Task<IEnumerable<Patient>> GetPatientByName(string patientName);
    }
}