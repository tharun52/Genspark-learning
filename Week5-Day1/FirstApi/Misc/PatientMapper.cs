using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Misc
{
    public class PatientMapper
    {
        public Patient? MapPatientAddRequestPatient(PatientAddRequestDto addRequestDto)
        {
            Patient patient = new();
            patient.Name = addRequestDto.Name;
            patient.Age = addRequestDto.Age;
            patient.Email = addRequestDto.Email;
            return patient;
        }
    }
}