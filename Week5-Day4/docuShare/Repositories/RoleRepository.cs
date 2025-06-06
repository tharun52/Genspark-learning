using docuShare.Contexts;
using docuShare.Models;
using FirstApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace docuShare.Repositories
{
    public class RoleRepository : Repository<string, Role>
    {
        public RoleRepository(DocumentContext documentContext) : base(documentContext)
        {
        }

        public override async Task<Role> Get(string key)
        {
            return await _documentContext.Roles.SingleOrDefaultAsync(r => r.Name == key)
                   ?? throw new KeyNotFoundException($"Role with name '{key}' not found.");
        }

        public override async Task<IEnumerable<Role>> GetAll()
        {
            return await _documentContext.Roles.ToListAsync();
        }
    }
}