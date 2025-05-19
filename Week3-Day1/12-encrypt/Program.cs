// 12) Write a program that:

// Takes a message string as input (only lowercase letters, no spaces or symbols).

// Encrypts it by shifting each character forward by 3 places in the alphabet.

// Decrypts it back to the original message by shifting backward by 3.

// Handles wrap-around, e.g., 'z' becomes 'c'.

// Examples
// Input:     hello
// Encrypted: khoor
// Decrypted: hello
// -------------
// Input:     xyz
// Encrypted: abc
// Test cases
// | Input | Shift | Encrypted | Decrypted |
// | ----- | ----- | --------- | --------- |
// | hello | 3     | khoor     | hello     |
// | world | 3     | zruog     | world     |
// | xyz   | 3     | abc       | xyz       |
// | apple | 1     | bqqmf     | apple     |
internal class Program
{
    static string getString(string prompt)
    {
        string input;

        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine().ToUpper();

            if (!string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter))
            {
                return input.ToLower();
            }

            Console.WriteLine("Invalid input. Please Try again.");
        }
    }

    static string encrypt(string s)
    {
        string encryptedstr = "";
        foreach (char c in s)
        {
            encryptedstr += (char)((c - 'a' + 3) % 26 + 'a');
        }
        return encryptedstr;
    }

    static string decrypt(string s)
    {
        string encryptedstr = "";
        foreach (char c in s)
        {
            encryptedstr += (char)((c - 'a' - 3) % 26 + 'a');
        }
        return encryptedstr;
    }

    static void Main()
    {
        string s = getString("Enter a string : ");

        string encrypt_s = encrypt(s);
        Console.WriteLine($"Encrpted string : {encrypt_s}");

        string decrypt_s = decrypt(encrypt_s);
        Console.WriteLine($"Encrpted string : {decrypt_s}");

    }
}