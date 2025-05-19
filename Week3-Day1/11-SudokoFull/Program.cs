// 10) write a program that accepts a 9-element array representing a Sudoku row.

// Validates if the row:

// Has all numbers from 1 to 9.

// Has no duplicates.

// Displays if the row is valid or invalid. 

// 11)  In the question ten extend it to validate a sudoku game. 
// Validate all 9 rows (use int[,] board = new int[9,9])

internal class Program
{
    static int[] getSudokoRow(int index)
    {
        string s = "";
        Console.WriteLine($"Enter Sudoku Row 1 (9 digits, no spaces): ");
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
                Console.Write($"Re-enter Enter Sudoku Row {index + 1}: ");
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
            if (num < 1 || num > 9 || seen[num - 1])
                return false;
            seen[num - 1] = true;
        }
        return true;
    }

    static bool checkSudokoColumn(int[,] board, int colIndex)
    {
        bool[] seen = new bool[9];
        for (int row = 0; row < 9; row++)
        {
            int num = board[row, colIndex];
            if (num < 1 || num > 9 || seen[num - 1])
                return false;
            seen[num - 1] = true;
        }
        return true;
    }

    static bool checkSudokoBlock(int[,] board, int startRow, int startCol)
    {
        bool[] seen = new bool[9];
        for (int row = startRow; row < startRow + 3; row++)
        {
            for (int col = startCol; col < startCol + 3; col++)
            {
                int num = board[row, col];
                if (num < 1 || num > 9 || seen[num - 1])
                    return false;
                seen[num - 1] = true;
            }
        }
        return true;
    }

    static bool checkFullSudoku(int[,] board)
    {
        for (int i = 0; i < 9; i++)
        {
            int[] row = new int[9];
            for (int j = 0; j < 9; j++)
            {
                row[j] = board[i, j];
            }
            if (!checkSudokoRow(row))
            {
                Console.WriteLine($"Invalid Row {i + 1}");
                return false;
            }
            if (!checkSudokoColumn(board, i))
            {
                Console.WriteLine($"Invalid Column {i + 1}");
                return false;
            }
        }
        for (int row = 0; row < 9; row += 3)
        {
            for (int col = 0; col < 9; col += 3)
            {
                if (!checkSudokoBlock(board, row, col))
                {
                    Console.WriteLine($"Invalid 3x3 Block starting at ({row + 1},{col + 1})");
                    return false;
                }
            }
        }
        return true;
    }
    // static void Main()
    // {
    //     int[,] board = new int[9, 9];

    //     for (int i = 0; i < 9; i++)
    //     {
    //         int[] row = getSudokoRow(i);
    //         for (int j = 0; j < 9; j++)
    //             board[i, j] = row[j];
    //     }

    //     Console.WriteLine("Sudoku Board Entered:");
    //     for (int i = 0; i < 9; i++)
    //     {
    //         for (int j = 0; j < 9; j++)
    //             Console.Write(board[i, j] + " ");
    //         Console.WriteLine();
    //     }

    //     Console.WriteLine("Validating Sudoku...");
    //     if (checkFullSudoku(board))
    //         Console.WriteLine("Sudoku board is valid.");
    //     else
    //         Console.WriteLine("Sudoku board is invalid.");
    // }
    static void Main()
    {
        int[,] board = new int[9, 9]
        {
        {2, 9, 5, 7, 4, 3, 8, 6, 1},
        {4, 3, 1, 8, 6, 5, 9, 2, 7},
        {8, 7, 6, 1, 9, 2, 5, 4, 3},
        {3, 8, 7, 4, 5, 9, 2, 1, 6},
        {6, 1, 2, 3, 8, 7, 4, 9, 5},
        {5, 4, 9, 2, 1, 6, 7, 3, 8},
        {7, 6, 3, 5, 2, 4, 1, 8, 9},
        {9, 2, 8, 6, 7, 1, 3, 5, 4},
        {1, 5, 4, 9, 3, 8, 6, 7, 2}
        };

        if (checkFullSudoku(board))
            Console.WriteLine("Sudoku board is valid.");
        else
            Console.WriteLine("Sudoku board is invalid.");
    }

}