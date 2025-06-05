

using docuShare.Contexts;
using docuShare.Models;
using FirstApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace docuShare.Repositories
{
    public class UserRepository : Repository<int, User>
    {
        
        public UserRepository(DocumentContext documentContext) : base(documentContext)
        {
        }
        public override async Task<User> Get(int key)
        {
            return await _documentContext.Users.SingleOrDefaultAsync(u => u.Id == key);

        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _documentContext.Users.ToListAsync();
        }
    }
}