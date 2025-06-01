using FirstApi.Contexts;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories
{
    public class DoctorSpecialityRepository : Repository<int, DoctorSpeciality>
    {
        public DoctorSpecialityRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<DoctorSpeciality> Get(int key)
        {
            var doctorSpeciality = await _clinicContext.DoctorSpecialities
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Speciality)
                .SingleOrDefaultAsync(ds => ds.SerialNumber == key);

            if (doctorSpeciality != null)
            {
                return doctorSpeciality;
            }
            else
            {
                throw new Exception($"No DoctorSpeciality with SerialNumber {key}");
            }
        }

        public override async Task<IEnumerable<DoctorSpeciality>> GetAll()
        {
            var doctorSpecialities = _clinicContext.DoctorSpecialities
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Speciality);

            if (!doctorSpecialities.Any())
            {
                throw new Exception("No DoctorSpecialities in the database");
            }
            return await doctorSpecialities.ToListAsync();
        }
    }
}