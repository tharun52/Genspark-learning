using System;

public class EmailService
{
    public void SendEmail(GoodStudent student, string subject, string body)
    {
        Console.WriteLine("\n--- Email Sent ---");
        Console.WriteLine($"To: {student.EmailAddress}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
    }
}
