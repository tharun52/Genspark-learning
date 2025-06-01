using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Interfaces
{
    public interface IDoctorService
    {
        public Task<ICollection<Doctor>> GetAllDoctors();
        public Task<ICollection<Doctor>> GetDoctByName(string name);
        public Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality);
        public Task<Doctor> AddDoctor(DoctorAddRequestDto doctor);

        public Task<Doctor> UpdateDoctor(DoctorAddRequestDto doctor);
        public Task<Doctor> DeleteDoctor(DoctorAddRequestDto doctor);
    }
}