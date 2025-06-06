using docuShare.Interfaces;
using docuShare.Models;
using docuShare.Models.DTOs;

namespace docuShare.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IRepository<string, Role> _roleRepository;

        public AuthenticationService(ITokenService tokenService,
                                    IEncryptionService encryptionService,
                                    IRepository<string, User> userRepository,
                                    IRepository<int, Employee> employeeRepository,
                                    IRepository<string, Role> roleRepository)
        {
            _tokenService = tokenService;
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }
        public async Task<UserLoginResponse> Login(UserLoginRequest user)
        {
            var dbUser = await _userRepository.Get(user.Username);
            if (dbUser == null)
            {
                throw new Exception("No such user");
            }

            var employees = await _employeeRepository.GetAll();
            var employee = employees.FirstOrDefault(e => e.User != null && e.User.UserName == dbUser.UserName);
            if (employee?.User?.Role == null)
            {  
                throw new Exception("Role not found for user");
            }
            dbUser.Role = employee.User.Role;

            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = user.Password,
                HashKey = dbUser.HashKey
            });
            if (encryptedData.EncryptedData == null)
            {
                throw new Exception("Encryption failed");
            }
            if (dbUser.Password == null)
            {
                throw new Exception("User password is not set");
            }
            for (int i = 0; i < encryptedData.EncryptedData.Length; i++)
            {
                if (encryptedData.EncryptedData[i] != dbUser.Password[i])
                {
                    throw new Exception("Invalid password");
                }
            }
            var token = await _tokenService.GenerateToken(dbUser);
            return new UserLoginResponse
            {
                Username = user.Username,
                Token = token,
            };
        }
    }
}