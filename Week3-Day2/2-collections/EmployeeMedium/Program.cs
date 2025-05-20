using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    static Dictionary<int, Employee> Employees = new Dictionary<int, Employee>();

    static void ShowMenu()
    {
        Console.WriteLine("\nMenu");
        Console.WriteLine("1. Add a Employee");
        Console.WriteLine("2. Display Employees");
        Console.WriteLine("3. Sort by Salary");
        Console.WriteLine("4. Find Employee by Id");
        Console.WriteLine("5. Find Employee by name");
        Console.WriteLine("6. Find Employee older than another employee");
        Console.WriteLine("7. Exit");
        Console.Write("Enter Your Choice : ");
    }

    static void AddEmployee()
    {
        Employee emp = new Employee();
        emp.TakeEmployeeDetailsFromUser();

        if (!Employees.ContainsKey(emp.Id))
        {
            Employees.Add(emp.Id, emp);
            Console.WriteLine("\nEmployee added successfully!");
        }
        else
        {
            Console.WriteLine("\nEmployee with this ID already exists!");
        }
    }

    static void DisplayAllEmployees()
    {
        Console.WriteLine("\nAll Employees:");
        foreach (KeyValuePair<int, Employee> ele in Employees)
        {
            Console.WriteLine("\n" + ele.Value);
        }
    }

    static void SortBySalary()
    {
        Console.WriteLine("\nEmployees Sorted by Salary:");
        List<Employee> sortedEmployees = new List<Employee>(Employees.Values);
        sortedEmployees.Sort();

        foreach (Employee e in sortedEmployees)
        {
            Console.WriteLine("\n" + e);
        }
    }

    static void FindById()
    {
        int searchId = ReadInt("\nEnter Employee Id you want to search : ");

        var matchedEmployee = Employees.Values.FirstOrDefault(e => e.Id == searchId);

        if (matchedEmployee != null)
        {
            Console.WriteLine("\nEmployee Found : ");
            Console.WriteLine(matchedEmployee);
        }
        else
        {
            Console.WriteLine($"Employee with ID {searchId} not found.");
        }
    }

    static void FindByName()
    {
        Console.Write("\nEnter the name of the Employee you want to search : ");
        string searchName = Console.ReadLine().ToLower();

        var matchedEmployees = Employees.Values.Where(e => e.Name.ToLower() == searchName);

        if (matchedEmployees.Any())
        {
            Console.WriteLine("\nEmployees Found:");
            foreach (var e in matchedEmployees)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            Console.WriteLine($"Employee with name '{searchName}' not found.");
        }
    }

    static void FindElders()
    {
        int searchIdForAge = ReadInt("\nEnter Employee Id you want to Compare Age : ");

        var matchedEmployeeForAge = Employees.Values.FirstOrDefault(e => e.Id == searchIdForAge);

        if (matchedEmployeeForAge != null)
        {
            Console.WriteLine($"\nSelected Employee: {matchedEmployeeForAge.Name} Age: {matchedEmployeeForAge.Age}\n");

            var olderEmployees = Employees.Values.Where(emp => emp.Age > matchedEmployeeForAge.Age);

            if (olderEmployees.Any())
            {
                Console.WriteLine("Employees older than selected employee:");
                foreach (var olderEmp in olderEmployees)
                {
                    Console.WriteLine(olderEmp);
                }
            }
            else
            {
                Console.WriteLine("No employees are older than the selected employee.");
            }
        }
        else
        {
            Console.WriteLine($"Employee with ID {searchIdForAge} not found.");
        }
    }

    static int ReadInt(string message)
    {
        int result;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.Write("Invalid Input! Please Try Again\n" + message);
        }
        return result;
    }

    static void Main()
    {
        int choice;
        while (true)
        {
            ShowMenu();
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
            {
                Console.Write("\nInvalid Input! Please Try Again\nEnter Your Choice : ");
            }

            switch (choice)
            {
                case 1: AddEmployee(); break;
                case 2: DisplayAllEmployees(); break;
                case 3: SortBySalary(); break;
                case 4: FindById(); break;
                case 5: FindByName(); break;
                case 6: FindElders(); break;
                case 7:
                    Console.WriteLine("Exiting...");
                    return;
            }
        }
    }
}
