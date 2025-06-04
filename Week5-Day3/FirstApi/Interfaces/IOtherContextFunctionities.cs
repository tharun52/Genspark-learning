using FirstApi.Models.DTOs;

namespace FirstApi.Interfaces
{
    public interface IOtherContextFunctionities
    {
        public Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string specilaity);
    }
}