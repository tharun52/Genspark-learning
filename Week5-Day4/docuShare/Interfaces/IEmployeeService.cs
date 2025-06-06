using docuShare.Models;
using docuShare.Models.DTOs;

namespace docuShare.Interfaces
{
    public interface IEmployeeService
    {
        public Task<Employee> AddEmployee(AddEmployeeDto employeeDto);
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<IEnumerable<Employee>> GetEmployeeByName(string employeeName);
    }
}