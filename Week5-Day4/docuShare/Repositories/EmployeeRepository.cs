using docuShare.Contexts;
using docuShare.Models;
using FirstApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace docuShare.Repositories
{
    public class EmployeeRepository : Repository<int, Employee>
    {
        public EmployeeRepository(DocumentContext documentContext) : base(documentContext)
        {
        }

        public override async Task<Employee> Get(int key)
        {
            return await _documentContext.Employees
                .Include(e => e.User)
                .ThenInclude(u => u.Role)
                .SingleOrDefaultAsync(e => e.Id == key)
                ?? throw new KeyNotFoundException($"Employee with ID '{key}' not found.");
        }

        public override async Task<IEnumerable<Employee>> GetAll()
        {
            return await _documentContext.Employees
                .Include(e => e.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
        }
    }
}