using AutoMapper;
using FirstApi.Interfaces;
using FirstApi.Misc;
using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;
        private IRepository<string, User> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        PatientMapper patientMapper;

        public PatientService(IRepository<int, Patient> patientRepository,
                              IRepository<string, User> userRepository,
                              IEncryptionService encryptionService,
                              IMapper mapper)
        {
            patientMapper = new PatientMapper();
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }
        public async Task<Patient> AddPatient(PatientAddRequestDto patientDto)
        {
            var user = _mapper.Map<PatientAddRequestDto, User>(patientDto);
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = patientDto.Password
            });
            user.Password = encryptedData.EncryptedData;
            user.HashKey = encryptedData.HashKey;
            user.Role = "Patient";
            user = await _userRepository.Add(user);
            var newpatient = patientMapper.MapPatientAddRequestPatient(patientDto);
            if (newpatient == null)
                throw new Exception("Failed to map patient DTO to Patient entity");
            newpatient.Status = "Active";
            newpatient = await _patientRepository.Add(newpatient);
            if (newpatient == null)
                throw new Exception("Patient not added");
            return newpatient;
        }
    }
}