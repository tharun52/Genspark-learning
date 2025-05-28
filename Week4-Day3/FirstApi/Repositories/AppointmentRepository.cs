using FirstApi.Contexts;
using FirstApi.Exceptions;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories
{
    public class AppointmentRepository : Repository<string, Appointment>
    {
        public AppointmentRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<Appointment> Get(string key)
        {
            var appointment = await _clinicContext.Appointmnets.SingleOrDefaultAsync(p => p.AppointmentNumber == key);

            if (appointment != null)
            {
                return appointment;
            }
            else
            {
                throw new Exception($"No Appointment with Appointment Number {key}");
            }
        }
        public override async Task<IEnumerable<Appointment>> GetAll()
        {
            var appointments = _clinicContext.Appointmnets;
            if (!appointments.Any())
            {
                throw new Exception("No appointments in the database");
            }
            return await appointments.ToListAsync();
        }
    }
}