// 1) create a program that will take name from user and greet the user 

// Console.Write("Enter name:");
// string name = Console.ReadLine();
// Console.WriteLine("Hello, "+name+"!");





// 2) Take 2 numbers from user and print the largest

// internal class Program
// {
//     private static void Main(string[] args)
//     {
//         static void islarge(int a, int b)
//         {
//             if (a > b)
//             {
//                 Console.WriteLine(a + " is greater than " + b);
//             }
//             else if (a == b)
//             {
//                 Console.WriteLine(a + " is equal to " + b);
//             }
//             else
//             {
//                 Console.WriteLine(b + " is greater than " + a);
//             }
//         }

//         Console.Write("Enter 1st number : ");
//         int a = Convert.ToInt32(Console.ReadLine());
//         Console.Write("Enter 2nd number : ");
//         int b = Convert.ToInt32(Console.ReadLine());
//         islarge(a, b);
//     }
// }




// 3) Take 2 numbers from user, check the operation user wants to perform (+,-,*,/). Do the operation and print the result

// internal class Program
// {
//     private static void Main(string[] args)
//     {
//         static float? Calculate(int num1, int num2, string? opInput)
//         {
//             if (string.IsNullOrEmpty(opInput) || opInput.Length != 1)
//             {
//                 Console.WriteLine("Error: Invalid operation input.");
//                 return null;
//             }

//             char operation = opInput[0];

//             switch (operation)
//             {
//                 case '+':
//                     return num1 + num2;

//                 case '-':
//                     return num1 - num2;

//                 case '*':
//                     return num1 * num2;

//                 case '/':
//                     if (num2 == 0)
//                     {
//                         Console.WriteLine("Error: Division by zero.");
//                         return null;
//                     }
//                     return (float)num1 / num2;

//                 default:
//                     Console.WriteLine("Error: Unsupported operation symbol.");
//                     return null;
//             }
//         }

//         Console.Write("Enter 1st number : ");
//         int a = Convert.ToInt32(Console.ReadLine());

//         Console.Write("Enter 2nd number : ");
//         int b = Convert.ToInt32(Console.ReadLine());

//         Console.Write("Enter Operation : ");
//         string? opInput = Console.ReadLine();

//         Console.WriteLine($"{a} {opInput[0]} {b} = {Calculate(a, b, opInput)}");
//     }
// }

// 4) Take username and password from user. Check if user name is "Admin" and password is "pass" if yes then print success message.
// Give 3 attempts to user. In the end of eh 3rd attempt if user still is unable to provide valid creds then exit the application after print the message 
// "Invalid attempts for 3 times. Exiting...."

// internal class Program
// {
//     private static void Main(string[] args)
//     {
//         static string CheckAdmin()
//         {
//             for (int i = 1; i <= 3; i++)
//             {
//                 Console.Write("Enter Username: ");
//                 string? username = Console.ReadLine();

//                 Console.Write("Enter Password: ");
//                 string? password = Console.ReadLine();

//                 if (string.Equals(username, "Admin") && string.Equals(password, "pass"))
//                 {
//                     return "Login Success";
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Invalid credentials. Attempts left: {3 - i}");
//                 }
//             }

//             return "Invalid attempts for 3 times. Exiting....";
//         }

//         string result = CheckAdmin();
//         Console.WriteLine(result);
//     }
// }


// 5) Take 10 numbers from user and print the number of numbers that are divisible by 7

// internal class Program
// {
//     private static void Main(string[] args)
//     {
//         int count = 0;

//         for (int i = 1; i <= 10; i++)
//         {
//             Console.Write($"Enter number {i} :");
//             if (int.TryParse(Console.ReadLine(), out int n))
//             {
//                 if (n % 7 == 0)
//                 {
//                     count++;
//                 }
//             }
//             else
//             {
//                 Console.WriteLine("Invalid input. Please enter an integer.");
//                 i--; 
//             }
//         }
//         Console.WriteLine("Count : " + count);
//     }
// }







