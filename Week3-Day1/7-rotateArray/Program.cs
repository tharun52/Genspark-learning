// 7) create a program to rotate the array to the left by one position.
// Input: {10, 20, 30, 40, 50}
// Output: {20, 30, 40, 50, 10}

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
    static void rotateArray(int[] arr)
    {
        int firstElement = arr[0];
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            arr[i] = arr[i + 1];
        }
        arr[n - 1] = firstElement;
    }
    static void Main()
    {
        int[] arr = getInputArray();
        Console.WriteLine("Original Array: " + string.Join(", ", arr));
        rotateArray(arr);
        Console.WriteLine("Rotated Array: " + string.Join(", ", arr));

    }
}
