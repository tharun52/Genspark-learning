// 1) Create a C# console application which has a class with name “EmployeePromotion” that will take employee names in the order in which they are eligible for promotion.  

// Example Input:  
// Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop) 
// Ramu 
// Bimu 
// Somu 
// Gomu 
// Vimu 

// Create a collection that will hold the employee names in the same order that they are inserted. 
// Hint – choose the correct collection that will preserve the input order (List) 

// 2) Use the application created for question 1 and in the same class do the following 
// Given an employee name find his position in the promotion list 

// Example Input:  
// Please enter the employee names in the order of their eligibility for promotion 
// Ramu 
// Bimu 
// Somu 
// Gomu 
// Vimu 
// Please enter the name of the employee to check promotion position 
// Somu 
// “Somu” is the the position 3 for promotion. 
// Hint – Choose the correct method that will give back the index (IndexOf) 

// 3)Use the application created for question 1 and in the same class do the following 
//  The application seems to be using some excess memory for storing the name, contain the space by using only the quantity of memory that is required. 
// Example Input:  
// Please enter the employee names in the order of their eligibility for promotion 
// Ramu 
// Bimu 
// Somu 
// Gomu 
// Vimu 
// The current size of the collection is 8 
// The size after removing the extra space is 5 
// Hint – List multiples the memory when we add elements, ensure you use only the size that is equal to the number of elements that are present. 

// 4) Use the application created for question 1 and in the same class do the following 
// The need for the list is over as all the employees are promoted. Not print all the employee names in ascending order. 

// Example Input:  
// Please enter the employee names in the order of their eligibility for promotion 
// Ramu 
// Bimu 
// Somu 
// Gomu 
// Vimu 
// Promoted employee list: 
// Bimu 
// Gomu 
// Ramu 
// Somu 
// Vimu 

// Refer EmployeePromotion.cs for code

internal class Program
{
    class EmployeePromotion
    {
        private List<string> employeeNames = new List<string>();

        public void TakePromotionListFromUser()
        {
            Console.WriteLine("\nPlease enter the employee names in the order of their eligibility for promotion (Please enter blank to stop)");

            while (true)
            {
                string? name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    break;
                }

                employeeNames.Add(name);
            }

        }

        public void PrintPromotionList()
        {
            Console.WriteLine("\nThe promotion order of employees is:");
            foreach (string name in employeeNames)
            {
                Console.WriteLine(name);
            }
        }

        public void GetPosition()
        {
            string? name;
            while (true)
            {
                Console.WriteLine("\nPlease enter the name of the employee to check promotion position");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }

            int position = employeeNames.IndexOf(name);
            if (position != -1)
            {
                Console.WriteLine($"\"{name}\" is in position {position + 1} for promotion.");
            }
            else
            {
                Console.WriteLine($"\"{name}\" is not found in the promotion list.");
            }
        }

        public void RemoveExcessMemory()
        {
            Console.WriteLine("The current size of the collection is " + employeeNames.Capacity);

            employeeNames.TrimExcess();

            Console.WriteLine("The size after removing the extra space is " + employeeNames.Capacity);
        }

        public void PrintPromotionListAsceding()
        {
            employeeNames.Sort();
            Console.WriteLine("\nPromoted employee list:");
            foreach (var name in employeeNames)
            {
                Console.WriteLine(name);
            }
        }
    }


    static void Main(string[] args)
    {
        EmployeePromotion promotionList = new EmployeePromotion();

        promotionList.TakePromotionListFromUser();
        promotionList.PrintPromotionList();
        promotionList.GetPosition();
        promotionList.RemoveExcessMemory();
        promotionList.PrintPromotionListAsceding();
    }
}