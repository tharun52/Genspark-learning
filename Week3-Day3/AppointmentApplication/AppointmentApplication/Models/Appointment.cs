using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppointmentApplication.Models
{
    public class Appointment : IComparable<Appointment>, IEquatable<Appointment>
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public Appointment(int id, string? patientName, int patientAge, DateTime appointmentDate, string? reason)
        {
            Id = id;
            PatientName = patientName;
            PatientAge = patientAge;
            AppointmentDate = appointmentDate;
            Reason = reason;
        }

        public Appointment()
        {
        }

        public void ReadAppointmentDetails()
        {
            Console.WriteLine("Enter Paitent Name : ");
            PatientName = Console.ReadLine() ?? string.Empty;
            
            Console.WriteLine("Enter Paitent Age : ");
            int age;
            while(!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Invalid input. \nPlease enter a valid age : ");
            }
            PatientAge = age;

            Console.WriteLine("Enter Appointment Date : ");
            DateTime date;
            while(!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Invalid input. \nPlease enter a valid date : ");
            }
            AppointmentDate = date;

            Console.WriteLine("Enter Reason for Appointment : ");
            Reason = Console.ReadLine() ?? string.Empty;
        }

        public override string ToString()
        {
            return "Appointment ID : " + Id + "\nPatient Name : " + PatientName + "\nAge : " + PatientAge + "\nDate : " + AppointmentDate + "\nReason : "+ Reason;
        }

        public int CompareTo(Appointment? other)
        {
            return this.Id.CompareTo(other?.Id);
        }
        public bool Equals(Appointment? other)
        {
            return this.Id == other?.Id;
        }
    }
}