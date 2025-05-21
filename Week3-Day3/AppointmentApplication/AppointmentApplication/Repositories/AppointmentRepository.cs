using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApplication.Exceptions;
using AppointmentApplication.Models;

namespace AppointmentApplication.Repositories
{
    public class AppointmentRepository : Repository<int, Appointment>
    {
        public AppointmentRepository() : base()
        {
        }
        public override ICollection<Appointment> GetAll()
        {
            if (_items.Count == 0)
            {
                throw new CollectionEmptyException("There are no Item present");
            }
            return _items;
        }

        public override Appointment GetById(int id)
        {
            var appointment = _items.FirstOrDefault(x => x.Id == id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("No Appointment Found");
            }
            return appointment;
        }

        protected override int GenerateId()
        {
            if (_items.Count == 0)
            {
                return 1;
            }
            else
            {
                return _items.Max(x => x.Id)+1;
            }
        } 
    }
}
