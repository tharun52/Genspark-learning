using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Misc
{
    public class SpecialityMapper
    {
        public Speciality? MapSpecialityAddRequestDoctor(SpecialityAddRequestDto addRequestDto)
        {
            Speciality speciality = new();
            speciality.Name = addRequestDto.Name;
            return speciality;
        }

        public DoctorSpeciality MapDoctorSpecility(int doctorId, int specialityId)
        {
            DoctorSpeciality doctorSpeciality = new();
            doctorSpeciality.DoctorId = doctorId;
            doctorSpeciality.SpecialityId = specialityId;
            return doctorSpeciality;
        }

    }
}