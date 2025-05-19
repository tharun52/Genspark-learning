// 10) write a program that accepts a 9-element array representing a Sudoku row.

// Validates if the row:

// Has all numbers from 1 to 9.

// Has no duplicates.

// Displays if the row is valid or invalid. 

internal class Program
{
    static int[] getSudokoRow()
    {
        string s = "";
        Console.WriteLine("Enter Sudoku Row (9 digits, no spaces): ");
        while (true)
        {
            s = Console.ReadLine();
            if (!string.IsNullOrEmpty(s) && s.Length == 9)
            {
                break;
            }
            else
            {
                Console.WriteLine("Wrong input. Please enter exactly 9 digits with no spaces.");
                Console.Write("Enter Sudoku Row: ");
            }
        }
        int[] arr = new int[9];
        for (int i = 0; i < 9; i++)
        {
            arr[i] = int.Parse(s[i].ToString());
        }
        return arr;
    }
    static bool checkSudokoRow(int[] row)
    {
        if (row.Length != 9)
            return false;

        bool[] seen = new bool[9];

        foreach (int num in row)
        {
            if (num < 1 || num > 9)
                return false;

            if (seen[num - 1])
                return false;

            seen[num - 1] = true;
        }
        return true;
    }
    static void Main()
    {
        int[] arr = getSudokoRow();

        if (checkSudokoRow(arr))
            Console.WriteLine("Valid Sudoku Row");
        else
            Console.WriteLine("Invalid Sudoku Row");
    }
}