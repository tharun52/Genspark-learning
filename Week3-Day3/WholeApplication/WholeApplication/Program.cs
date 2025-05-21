using WholeApplication.Interfaces;
using WholeApplication.Models;
using WholeApplication.Repositories;
using WholeApplication.Services;

namespace WholeApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<int, Employee> employeeRepository = new EmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);


            while (true)
            {
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Search for Employee");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Please enter your choice (1-3) : ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Employee employee1 = new Employee();
                        employee1.TakeEmployeeDetailsFromUser();
                        int id = employeeService.AddEmployee(employee1);
                        if (id != -1)
                        {
                            Console.WriteLine("Employee added successfully with ID: " + id);
                        }
                        else
                        {
                            Console.WriteLine("Failed to add employee.");
                        }
                        break;
                    case "2":
                        SearchModel searchModel = new SearchModel();

                        Console.Write("Enter ID to search (or leave blank): ");
                        var idInput = Console.ReadLine();
                        if (int.TryParse(idInput, out int searchId))
                            searchModel.Id = searchId;

                        Console.Write("Enter Name to search (or leave blank): ");
                        var nameInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(nameInput))
                            searchModel.Name = nameInput;

                        Console.Write("Enter Min Age to search (or leave blank): ");
                        var minAgeInput = Console.ReadLine();
                        Console.Write("Enter Max Age (or leave blank): ");
                        var maxAgeInput = Console.ReadLine();
                        if (int.TryParse(minAgeInput, out int minAge) || int.TryParse(maxAgeInput, out int maxAge))
                        {
                            searchModel.Age = new Range<int>
                            {
                                MinVal = int.TryParse(minAgeInput, out minAge) ? minAge : int.MinValue,
                                MaxVal = int.TryParse(maxAgeInput, out maxAge) ? maxAge : int.MaxValue
                            };
                        }

                        Console.Write("Enter Min Salary to search (or leave blank): ");
                        var minSalaryInput = Console.ReadLine();
                        Console.Write("Enter Max Salary (or leave blank): ");
                        var maxSalaryInput = Console.ReadLine();
                        if (double.TryParse(minSalaryInput, out double minSalary) || double.TryParse(maxSalaryInput, out double maxSalary))
                        {
                            searchModel.Salary = new Range<double>
                            {
                                MinVal = double.TryParse(minSalaryInput, out minSalary) ? minSalary : double.MinValue,
                                MaxVal = double.TryParse(maxSalaryInput, out maxSalary) ? maxSalary : double.MaxValue
                            };
                        }

                        var results = employeeService.SearchEmployee(searchModel);
                        if (results != null && results.Count > 0)
                        {
                            Console.WriteLine("\n--- Search Results ---");
                            foreach (var emp in results)
                            {
                                Console.WriteLine(emp);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No employees found matching the criteria.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}