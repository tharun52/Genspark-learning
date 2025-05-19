// 8) Given two integer arrays, merge them into a single array.
// Input: {1, 3, 5} and {2, 4, 6}
// Output: {1, 3, 5, 2, 4, 6}

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
    static int[] mergeArray(int[] arr1, int[] arr2)
    {
        int n1 = arr1.Length;
        int n2 = arr2.Length;
        int[] newarr = new int[n1 + n2];
        for (int i = 0; i < n1; i++)
        {
            newarr[i] = arr1[i];
        }
        for (int i = 0; i < n2; i++)
        {
            newarr[n1 + i] = arr2[i];
        }
        return newarr;
    }
    static void Main()
    {
        Console.WriteLine("Enter Array 1");
        int[] arr1 = getInputArray();
        Console.WriteLine("Enter Array 2");
        int[] arr2 = getInputArray();
        int[] newarr = mergeArray(arr1, arr2);
        Console.WriteLine("Array 1 : [" + string.Join(", ", arr1)+"]");
        Console.WriteLine("Array 2 : [" + string.Join(", ", arr2)+"]");
        Console.WriteLine("Merged Array : [" + string.Join(", ", newarr)+"]");
    }
}