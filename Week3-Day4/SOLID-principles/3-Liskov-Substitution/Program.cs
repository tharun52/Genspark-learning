class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Bad Design ===");
        Teacher badTeacher = new Teacher();
        badTeacher.Teach();  

        Teacher badAdmin = new Administrator();
        try
        {
            badAdmin.Teach();  // Child class Administrator is not supposed to teach so expection
        }
        catch (NotImplementedException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\n=== Good Design ===");
        Employee goodTeacher = new GoodTeacher();
        Employee goodAdmin = new GoodAdministrator();

        goodTeacher.Work();  
        goodAdmin.Work();   
    }
}