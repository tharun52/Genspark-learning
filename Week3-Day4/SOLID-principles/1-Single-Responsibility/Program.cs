using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\nStudent Class with bad design for Single responsibility Principle : ");
        BadStudent badstud = new BadStudent(1, "John Doe", "john.doe@example.com");

        badstud.Save();
        badstud.SendEmail("Welcome to Class", "Hello John, welcome to your first day!");
        badstud.Enrol("C# Fundamentals");

        Console.WriteLine("\nStudent Class with Correct design for Single responsibility Principle : ");
        GoodStudent goodstud = new GoodStudent(1, "Alice Smith", "alice@example.com");

        StudentRepository repo = new StudentRepository();
        EmailService emailService = new EmailService();
        EnrollmentService enrollmentService = new EnrollmentService();

        repo.Save(goodstud);
        emailService.SendEmail(goodstud, "Welcome to SRP", "Hi Alice, you've successfully registered!");
        enrollmentService.Enroll(goodstud, "SOLID Principles in C#");
    }
}
