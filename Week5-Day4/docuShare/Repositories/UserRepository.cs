using docuShare.Contexts;
using docuShare.Models;
using FirstApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace docuShare.Repositories
{
    public class UserRepository : Repository<string, User>
    {
        
        public UserRepository(DocumentContext documentContext) : base(documentContext)
        {
        }
        public override async Task<User> Get(string key)
        {
            return await _documentContext.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.UserName == key) 
                   ?? throw new KeyNotFoundException($"User with username '{key}' not found.");

        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _documentContext.Users
                .Include(u => u.Role)
                .ToListAsync();
        }
    }
}