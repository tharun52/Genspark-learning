using FirstApi.Contexts;
using FirstApi.Exceptions;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories
{
    public class DoctorRepository : Repository<int, Doctor>
    {
        public DoctorRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<Doctor> Get(int key)
        {
            var doctor = await _clinicContext.Doctors.SingleOrDefaultAsync(p => p.Id == key);

            return doctor??throw new Exception("No doctor with the given ID");
        }

        public override async Task<IEnumerable<Doctor>> GetAll()
        {
            var doctors = _clinicContext.Doctors;
            if (doctors.Count() == 0)
                throw new Exception("No Doctor in the database");
            return (await doctors.ToListAsync());
        }
    }
}