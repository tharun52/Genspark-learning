using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<int, Doctor> _doctorRepository;
        private readonly IRepository<int, Speciality> _specialityRepository;
        private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;
        public DoctorService(IRepository<int, Doctor> doctorRepository,
                            IRepository<int, Speciality> specialityRepository,
                            IRepository<int, DoctorSpeciality> doctorSpecialityRepository)
        {
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _doctorSpecialityRepository = doctorSpecialityRepository;
        }

        public async Task<ICollection<Doctor>> GetAllDoctors()
        {
            var allDoctors = await _doctorRepository.GetAll();
            if (allDoctors.Any())
            {
                return allDoctors.ToList();
            }
            else
            {
                throw new Exception("No Doctors in the database");
            }
        }
        public async Task<ICollection<Doctor>> GetDoctByName(string name)
        {
            var allDoctors = await _doctorRepository.GetAll();
            var doctors = allDoctors.Where(d => d.Name == name);
            if (doctors.Any())
            {
                return doctors.ToList();
            }
            else
            {
                throw new Exception($"No Doctor with name {name}");
            }
        }
        public async Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality)
        {

            var allSpecialities = await _specialityRepository.GetAll();
            var specialityEntity = allSpecialities.FirstOrDefault(s => s.Name == speciality);
            if (specialityEntity == null)
            {
                throw new Exception($"No Speciality with name {speciality}");
            }

            var allDoctors = await _doctorRepository.GetAll();
            var allDoctorSpecialities = await _doctorSpecialityRepository.GetAll();
            var doctorIds = allDoctorSpecialities
                .Where(ds => ds.SpecialityId == specialityEntity.Id)
                .Select(ds => ds.DoctorId)
                .ToList();

            var doctors = allDoctors.Where(d => doctorIds.Contains(d.Id));
            if (doctors.Any())
            {
                return doctors.ToList();
            }
            else
            {
                throw new Exception($"No Doctors found for Speciality {speciality}");
            }
        }

        public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctorDto)
        {
            var doctor = new Doctor
            {
                Name = doctorDto.Name,
                Status = "Active", // Default or based on business logic
                YearsOfExperience = doctorDto.YearsOfExperience
            };

            doctor = await _doctorRepository.Add(doctor);

            var allSpecialities = await _specialityRepository.GetAll();
            var doctorSpecialities = new List<DoctorSpeciality>();

            foreach (var specialityDto in doctorDto.Specialities ?? new List<SpecialityAddRequestDto>())
            {
                var speciality = allSpecialities
                    .FirstOrDefault(s => s.Name.Equals(specialityDto.Name, StringComparison.OrdinalIgnoreCase));

                if (speciality == null)
                {
                    speciality = new Speciality
                    {
                        Name = specialityDto.Name,
                        Status = "Active" 
                    };

                    speciality = await _specialityRepository.Add(speciality);
                }

                var doctorSpeciality = new DoctorSpeciality
                {
                    DoctorId = doctor.Id,
                    SpecialityId = speciality.Id
                };

                await _doctorSpecialityRepository.Add(doctorSpeciality);
            }

            return doctor;
        }

        public Task<Doctor> UpdateDoctor(DoctorAddRequestDto doctor)
        {
            throw new NotImplementedException();
        }
        public Task<Doctor> DeleteDoctor(DoctorAddRequestDto doctor)
        {
            throw new NotImplementedException();
        }
    }
}

