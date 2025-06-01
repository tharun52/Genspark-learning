using FirstApi.Contexts;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories
{
    public class SpecialityRepository : Repository<int, Speciality>
    {
        public SpecialityRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<Speciality> Get(int key)
        {
            var speciality = await _clinicContext.Specialities.SingleOrDefaultAsync(s => s.Id == key);

            if (speciality != null)
            {
                return speciality;
            }
            else
            {
                throw new Exception($"No Speciality with Id {key}");
            }
        }

        public override async Task<IEnumerable<Speciality>> GetAll()
        {
            var list = await _clinicContext.Specialities.ToListAsync();
            return list;
        }
    }
}