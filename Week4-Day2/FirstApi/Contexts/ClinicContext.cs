using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Contexts
{
    public class ClinicContext : DbContext
    {
      
        public ClinicContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointmnet> Appointmnets { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }

    }
}
