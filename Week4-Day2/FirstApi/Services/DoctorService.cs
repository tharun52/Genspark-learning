using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Interfaces;
using FirstApi.Models;

namespace FirstApi.Services
{
    public class DoctorService : IDoctorService
    {
        IRepository<int, Doctor> _doctorRepository;
        public DoctorService(IRepository<int, Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public int AddDoctor(Doctor doctor)
        {
            try
            {
                var result = _doctorRepository.Add(doctor);
                if (result != null)
                {
                    return result.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public int DeleteDoctor(int id)
        {
            try
            {
                var result = _doctorRepository.Delete(id);
                if (result != null)
                {
                    return result.Id;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public int UpdateDoctor(Doctor doctor)
        {
            try
            {
                var result = _doctorRepository.Update(doctor);
                if (result != null)
                {
                    return result.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public ICollection<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAll();
        }
    }
}