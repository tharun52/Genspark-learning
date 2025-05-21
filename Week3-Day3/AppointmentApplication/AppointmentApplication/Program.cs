using AppointmentApplication.Interfaces;
using AppointmentApplication.Models;
using AppointmentApplication.Repositories;
using AppointmentApplication.Services;

namespace WholeApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<int, Appointment> appointentRepository = new AppointmentRepository();
            IAppointmentService appointmentService = new AppointmentService(appointentRepository);

            while (true)
            {
                DisplayMenu();
                var choice = Console.ReadLine();
                Appointment appointment = new Appointment();
                switch (choice)
                {
                    case "1":
                        AddAppointmentForUser(appointment, appointmentService);
                        break;
                    case "2":
                        SearchAppointmentForUser(appointment, appointmentService);
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("1. Add Appointment");
            Console.WriteLine("2. Search Appointment");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
        }
        
        static void AddAppointmentForUser(Appointment appointment, IAppointmentService appointmentService)
        {
            appointment.ReadAppointmentDetails();
            int id = appointmentService.AddAppointment(appointment);
            if(id != -1)
            {
                Console.WriteLine("\nAppointment Added Successfully");
            }
            else
            {
                Console.WriteLine("\nFailed to Add Employee");
            }
        }

        static void SearchAppointmentForUser(Appointment appointment, IAppointmentService appointmentService)
        {
            AppointmentSearchModel appointmentSearchModel = new AppointmentSearchModel();

            Console.Write("Enter ID to search (or leave blank): ");
            var idInput = Console.ReadLine();
            if (int.TryParse(idInput, out int searchId))
                appointmentSearchModel.Id = searchId;

            Console.Write("Enter Name to search (or leave blank): ");
            var nameInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(nameInput))
                appointmentSearchModel.PatientName = nameInput;

            Console.Write("Enter Min Age to search (or leave blank): ");
            var minAgeInput = Console.ReadLine();
            Console.Write("Enter Max Age (or leave blank): ");
            var maxAgeInput = Console.ReadLine();
            if (int.TryParse(minAgeInput, out int minAge) || int.TryParse(maxAgeInput, out int maxAge))
            {
                appointmentSearchModel.Age = new AppointmentSearchModel.Range<int>
                {
                    MinVal = int.TryParse(minAgeInput, out minAge) ? minAge : int.MinValue,
                    MaxVal = int.TryParse(maxAgeInput, out maxAge) ? maxAge : int.MaxValue
                };
            }

            Console.Write("Enter From Date to search (or leave blank): ");
            var fromDateInput = Console.ReadLine();
            Console.Write("Enter To Date (or leave blank): ");
            var toDateInput = Console.ReadLine();
            if (DateTime.TryParse(fromDateInput, out DateTime fromDate) || DateTime.TryParse(toDateInput, out DateTime toDate))
            {
                appointmentSearchModel.AppointmentDateRange = new AppointmentSearchModel.Range<DateTime>
                {
                    MinVal = DateTime.TryParse(fromDateInput, out fromDate) ? fromDate : DateTime.MinValue,
                    MaxVal = DateTime.TryParse(toDateInput, out toDate) ? toDate : DateTime.MaxValue
                };
            }

            var results = appointmentService.SearchAppointment(appointmentSearchModel);
            if (results != null && results.Count > 0)
            {
                Console.WriteLine("\n--- Search Results ---");
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine("No appointment found matching the criteria.");
            }
        }
    }
}
