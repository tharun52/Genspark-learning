using docuShare.Interfaces;
using docuShare.Misc;
using docuShare.Models;
using docuShare.Models.DTOs;
using docuShare.Repositories;

namespace docuShare.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IRepository<string, User> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<string, Role> _roleRepository;
        private readonly EmployeeMapper _employeeMapper;

        public EmployeeService(
            IRepository<int, Employee> employeeRepository,
            IRepository<string, User> userRepository,
            IRepository<string, Role> roleRepository,
            IEncryptionService encryptionService)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _roleRepository = roleRepository;
            _employeeMapper = new EmployeeMapper();
        }

        public async Task<Employee> AddEmployee(AddEmployeeDto employeeDto)
        {
            var newEmployee = _employeeMapper.MapAddEmployeeDtoToEmployee(employeeDto);
            if (newEmployee == null)
            {
                throw new ArgumentNullException(nameof(employeeDto), "Employee DTO cannot be null");
            }
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = employeeDto.Password
            });
            var roles = await _roleRepository.GetAll();
            if (roles == null || !roles.Any())
            {
                throw new InvalidOperationException("No roles available to assign to the employee.");
            }
            var role = roles.FirstOrDefault(r => r.Name.Equals(employeeDto.RoleName, StringComparison.OrdinalIgnoreCase));
            if (role == null)
            {
                throw new KeyNotFoundException($"Role '{employeeDto.RoleName}' not found.");
            }
            var user = new User
            {
                UserName = employeeDto.Email,
                Password = encryptedData.EncryptedData,
                HashKey = encryptedData.HashKey,
                Role = role
            };            
            newEmployee.User = user;
            newEmployee.Status = "Active";
            return await _employeeRepository.Add(newEmployee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAll();
            return employees;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByName(string employeeName)
        {
            var employees = await _employeeRepository.GetAll();
            if (employees == null || !employees.Any())
                return new List<Employee>();
            return employees.Where(e => e.Name.Contains(employeeName, StringComparison.OrdinalIgnoreCase));
        }
    }
}