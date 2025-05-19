// 9) Write a program that:

// Has a predefined secret word (e.g., "GAME").

// Accepts user input as a 4-letter word guess.

// Compares the guess to the secret word and outputs:

// X Bulls: number of letters in the correct position.

// Y Cows: number of correct letters in the wrong position.

// Continues until the user gets 4 Bulls (i.e., correct guess).

// Displays the number of attempts.

// Bull = Correct letter in correct position.

// Cow = Correct letter in wrong position.

// Secret Word	User Guess	Output	Explanation
// GAME	GAME	4 Bulls, 0 Cows	Exact match
// GAME	MAGE	1 Bull, 3 Cows	A in correct position, MGE misplaced
// GAME	GUYS	1 Bull, 0 Cows	G in correct place, rest wrong
// GAME	AMGE	2 Bulls, 2 Cows	A, E right; M, G misplaced
// NOTE	TONE	2 Bulls, 2 Cows	O, E right; T, N misplaced

internal class Program
{
    static bool compareString(string s1, string s2)
    {
        string crtChars = "";
        string wrngChars = "";
        for (int i = 0; i < 4; i++)
        {
            if (s1[i] == s2[i])
            {
                crtChars += s2[i];
            }
            else
            {
                wrngChars += s2[i];
            }
        }
        if (crtChars.Length == 4)
        {
            Console.WriteLine($"{s1} {s2} {crtChars.Length} Bulls, {wrngChars.Length} Cows, Exact Match.");
            return true;
        }
        else if (wrngChars.Length == 4)
        {
            Console.WriteLine($"{s1} {s2} {crtChars.Length} Bulls, {wrngChars.Length} Cows, No Character Matched.");
            return false;
        }
        else
        {
            Console.WriteLine($"{s1} {s2} {crtChars.Length} Bulls, {wrngChars.Length} Cows, {crtChars} in correct position, {wrngChars} misplaced.");
            return false;
        }
    }

    static string get4LetterInput(string prompt)
    {
        string input;

        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine().ToUpper();

            if (!string.IsNullOrWhiteSpace(input) && input.Length == 4)
            {
                return input;
            }

            Console.WriteLine("Invalid input. Please Try again.");
        }
    }
    static void Main()
    {
        string s1 = get4LetterInput("Enter your first word : ");
        string s2 = get4LetterInput("Enter your second word : ");
        for (int i = 0; i < 4; i++)
        {
            if (compareString(s1, s2))
            {
                Console.WriteLine("You won!");
                break;
            }
            else
            {   
                s2 = get4LetterInput("Enter your second word : ");
                Console.WriteLine("Try Again");    
            }
        }
    }
}

