using System;

public interface IMessageService
{
    void Send(Student student, string message);
}

// uses abstraction
public class EmailMessageService : IMessageService
{
    public void Send(Student student, string message)
    {
        Console.WriteLine($"[Email] To: {student.Name}, Message: {message}");
    }
}

// uses abstraction
public class SMSMessageService : IMessageService
{
    public void Send(Student student, string message)
    {
        Console.WriteLine($"[SMS] To: {student.Name}, Message: {message}");
    }
}

// Since EmailMessageService and SMSMessageService are both using Abstraction
// and GoodStudentNotifier also depends on abstraction
// we can create a new service without changing the GoodStudentNotifier class 
public class GoodStudentNotifier
{
    private readonly List<IMessageService> _messageServices;

    public GoodStudentNotifier(List<IMessageService> messageServices)
    {
        _messageServices = messageServices;
    }

    public void Notify(Student student)
    {
        foreach (var service in _messageServices)
        {
            service.Send(student, "Your result is available.");
        }
    }
}
