using System;

// Low-level modules
public class BadEmailService
{
    public void SendEmail(Student student, string message)
    {
        Console.WriteLine($"[Email] To: {student.Name}, Message: {message}");
    }
}

public class BadSMSService
{
    public void SendSMS(Student student, string message)
    {
        Console.WriteLine($"[SMS] To: {student.Name}, Message: {message}");
    }
}

public class BadStudentNotifier
{
    private BadEmailService _emailService;
    private BadSMSService _smsService;

    public BadStudentNotifier()
    {
        _emailService = new BadEmailService();  
        _smsService = new BadSMSService();      // This is bad
    }

    public void Notify(Student student)
    {
        _emailService.SendEmail(student, "Your result is available.");
        _smsService.SendSMS(student, "Your result is available.");
    }
}
