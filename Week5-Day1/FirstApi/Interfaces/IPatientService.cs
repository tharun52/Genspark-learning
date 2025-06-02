using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Interfaces
{
    public interface IPatientService
    {
        public Task<Patient> AddPatient(PatientAddRequestDto patientDto);
    }
}