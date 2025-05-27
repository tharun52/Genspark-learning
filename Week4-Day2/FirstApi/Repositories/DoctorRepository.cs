using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Repositories;
using FirstApi.Exceptions;
using FirstApi.Models;

namespace FirstApi.Repositories
{
    public class DoctorRepository : Repository<int, Doctor>
    {
        public DoctorRepository() : base()
        {
        }

        public override ICollection<Doctor> GetAll()
        {
            if (_items.Count == 0)
            {
                throw new CollectionEmptyException("No Doctor Found");
            }
            return _items;
        }
        public override Doctor GetById(int id)
        {
            var doctor = _items.FirstOrDefault(x => x.Id == id);
            if (doctor == null)
            {
                throw new KeyNotFoundException("No Doctor found by that Id");
            }
            return doctor;
        }
        public override int GenerateId()
        {
            if (_items.Count == 0)
            {
                return 1;
            }
            return _items.Max(x => x.Id) + 1;
        }
    }
}