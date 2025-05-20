using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    static int isElementFound(Dictionary<int, Employee> employees)
    {
        Console.Write("\nEnter Employee Id to search: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (employees.ContainsKey(id))
            {
                return id;
            }
        }

        return -1;
    }

    static void AddEmployee(Dictionary<int, Employee> employees)
    {
        Employee emp = new Employee();
        emp.TakeEmployeeDetailsFromUser();

        if (!employees.ContainsKey(emp.Id))
        {
            employees.Add(emp.Id, emp);
            Console.WriteLine("\nEmployee added successfully!");
        }
        else
        {
            Console.WriteLine("\nEmployee with this ID already exists!");
        }
    }

    static void SearchEmployee(Dictionary<int, Employee> employees)
    {
        int id = isElementFound(employees);
        if (id != -1)
        {
            Console.WriteLine("\nEmployee Found:");
            Console.WriteLine(employees[id]);
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }

    static void ModifyEmployee(Dictionary<int, Employee> employees)
    {
        int idToUpdate = isElementFound(employees);
        if (idToUpdate != -1)
        {
            var employeeToUpdate = employees.Values.FirstOrDefault(e => e.Id == idToUpdate);

            Console.Write("Enter New Name : ");
            string name = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(name))
                {
                    Console.Write("Invalid Input. Try Again\nEnter New Name : ");
                    name = Console.ReadLine();
                }
                else
                {
                    employeeToUpdate.Name = name;
                    break;
                }
            }

            int age, salary;

            Console.Write("Enter New Age : ");
            while (!int.TryParse(Console.ReadLine(), out age) || age < 0)
                Console.Write("Invalid Age\nEnter New Age : ");
            employeeToUpdate.Age = age;

            Console.Write("Enter New Salary : ");
            while (!int.TryParse(Console.ReadLine(), out salary) || salary < 0)
                Console.Write("Invalid Salary\nEnter New Salary : ");
            employeeToUpdate.Salary = salary;
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }

    static void DeleteEmployee(Dictionary<int, Employee> employees)
    {
        int idToDelete = isElementFound(employees);
        if (idToDelete != -1)
        {
            employees.Remove(idToDelete);
            Console.WriteLine($"Employee with ID {idToDelete} has been deleted successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }

    static void DisplayAllEmployees(Dictionary<int, Employee> employees)
    {
        Console.WriteLine("\nAll Employees:");
        foreach (KeyValuePair<int, Employee> ele in employees)
        {
            Console.WriteLine("\n" + ele.Value);
        }
    }

    static void Main()
    {
        Dictionary<int, Employee> Employees = new Dictionary<int, Employee>();
        int n;

        while (true)
        {
            Console.WriteLine("\nMenu");
            Console.WriteLine("1. Add a Employee");
            Console.WriteLine("2. Search a Employee");
            Console.WriteLine("3. Modify Employee");
            Console.WriteLine("4. Get Employee");
            Console.WriteLine("5. Display All Employees");
            Console.WriteLine("6. Exit");
            Console.Write("Enter Your Choice : ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 6)
            {
                Console.Write("\nInvalid Input! Please Try Again\nEnter Your Choice : ");
            }

            switch (n)
            {
                case 1: AddEmployee(Employees); break;
                case 2: SearchEmployee(Employees); break;
                case 3: ModifyEmployee(Employees); break;
                case 4: DeleteEmployee(Employees); break;
                case 5: DisplayAllEmployees(Employees); break;
                case 6:
                    Console.WriteLine("Exiting...");
                    return;
            }
        }
    }
}
