// 6) Count the Frequency of Each Element
// Given an array, count the frequency of each element and print the result.
// Input: {1, 2, 2, 3, 4, 4, 4}

// output
// 1 occurs 1 times  
// 2 occurs 2 times  
// 3 occurs 1 times  
// 4 occurs 3 times



internal class Program
{
static int[] getInputArray()
{
    int n;

    Console.Write("Enter no of elements : ");
    while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
    {
        Console.WriteLine("Invalid input. Please enter a positive integer.");
        Console.Write("Enter no of elements : ");
    }

    int[] arr = new int[n];

    for (int i = 0; i < n; i++)
    {
        Console.Write($"Enter Number {i + 1} : ");
        while (!int.TryParse(Console.ReadLine(), out arr[i]))
        {
            Console.WriteLine("Invalid input. Please try again.");
            Console.Write($"Enter Number {i + 1} : ");
        }
    }

    return arr;
}

    static void getElementCount(int[] arr)
    {
        int[] newArr = new int[arr.Length];
        int n = arr.Length;
        bool[] visited = new bool[n];

        for (int i = 0; i < n; i++)
        {
            if (visited[i])
                continue;

            int count = 1;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[i] == arr[j])
                {
                    count++;
                    visited[j] = true;
                }
            }

            Console.WriteLine($"{arr[i]} occurs {count} times");
        }
    }
    private static void Main(string[] args)
    {
        int[] arr = getInputArray();

        getElementCount(arr);
    }
}
