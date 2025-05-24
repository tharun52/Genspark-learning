using System;
class Program
{
    public static void Main(string[] args)
    {
        string path;
        while (true)
        {
            Console.WriteLine("Enter line path : ");
            path = Console.ReadLine();
            if (path == null || path.Substring(path.Length - 4, 4) == ".txt")
            {
                break;
            }
            Console.WriteLine("File not found or invalid extension (use .txt)");
        }

        var fileManage = Filemanager.GetInstance(path);
        System.Console.WriteLine("\nIntial content : ");
        fileManage.ReadFile();

        fileManage.WriteFile();

        System.Console.WriteLine("Content after writing : ");
        fileManage.ReadFile();
        fileManage.Close();
    }
}

