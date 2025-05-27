using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Models;
using FirstApi.Interfaces;

namespace FirstApi.Interfaces
{
    public interface IDoctorService
    {
        int AddDoctor(Doctor doctor);
        int DeleteDoctor(int id);
        int UpdateDoctor(Doctor doctor);
        ICollection<Doctor> GetAllDoctors();
    }
}