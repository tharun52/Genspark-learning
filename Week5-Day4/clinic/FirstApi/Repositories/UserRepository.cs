using FirstApi.Contexts;
using FirstApi.Interfaces;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories
{
    public class UserRepository : Repository<string, User>
    {
        public UserRepository(ClinicContext context):base(context)
        {
            
        }
        public override async Task<User> Get(string key)
        {
            return await _clinicContext.Users.SingleOrDefaultAsync(u => u.Username == key);
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _clinicContext.Users.ToListAsync();
        }
            

        // public async Task<User?> GetUserByEmailAsync(string email)
        // {
        //     return await _context.Users
        //         .Include(u => u.Doctor)
        //         .Include(u => u.Patient)
        //         .FirstOrDefaultAsync(u => u.Username == email);
        // }

        // public async Task AddUserAsync(User user)
        // {
        //     _context.Users.Add(user);
        //     await _context.SaveChangesAsync();
        // }
    }
}