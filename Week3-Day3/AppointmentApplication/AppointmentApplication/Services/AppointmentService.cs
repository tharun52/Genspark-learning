using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApplication.Interfaces;
using AppointmentApplication.Models;

namespace AppointmentApplication.Services
{
    public class AppointmentService : IAppointmentService
    {
        IRepository<int, Appointment> _appointmentRepository;

        public AppointmentService(IRepository<int, Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public int AddAppointment(Appointment appointment)
        {
            try
            {
                var result = _appointmentRepository.Add(appointment);
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

        public List<Appointment>? SearchAppointment(AppointmentSearchModel searchModel)
        {
            try
            {
                var appointments = _appointmentRepository.GetAll();
                appointments = SearchById(appointments, searchModel.Id);
                appointments = SearchByName(appointments, searchModel.PatientName);
                appointments = SearchByAge(appointments, searchModel.Age);
                appointments = SearchByDate(appointments, searchModel.AppointmentDateRange);
                if(appointments != null || appointments.Count>0)
                {
                    return appointments.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private ICollection<Appointment> SearchById(ICollection<Appointment> appointments, int? id)
        {
            if (id == null || appointments == null || appointments.Count == 0)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(x => x.Id == id).ToList();
            }
        }

        private ICollection<Appointment> SearchByName(ICollection<Appointment> appointments, string? name)
        {
            if (string.IsNullOrEmpty(name) || appointments == null || appointments.Count == 0)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(x => x.PatientName.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        private ICollection<Appointment> SearchByAge(ICollection<Appointment> appointments, AppointmentSearchModel.Range<int>? age)
        {
            if (age == null || appointments == null || appointments.Count == 0)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(e => e.PatientAge >= age.MinVal && e.PatientAge <= age.MaxVal).ToList();
            }
        }

        private ICollection<Appointment> SearchByDate(ICollection<Appointment> appointments, AppointmentSearchModel.Range<DateTime>? date)
        {
            if (date == null || appointments == null || appointments.Count == 0)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(e => e.AppointmentDate >= date.MinVal && e.AppointmentDate <= date.MaxVal).ToList();
            }
        }
    }
}
