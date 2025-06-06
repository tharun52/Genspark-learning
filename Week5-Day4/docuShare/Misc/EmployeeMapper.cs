using docuShare.Models;
using docuShare.Models.DTOs;

namespace docuShare.Misc
{
    public class EmployeeMapper
    {
        public Employee MapAddEmployeeDtoToEmployee(AddEmployeeDto dto)
        {
            if (dto == null) return null!;
            return new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Status = "Active"
            };
        }
    }
}