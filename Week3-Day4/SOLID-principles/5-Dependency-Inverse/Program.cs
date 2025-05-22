using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Student student = new Student("John Doe", 92);

        Console.WriteLine("=== BAD DESIGN ===");
        BadStudentNotifier badNotifier = new BadStudentNotifier();
        badNotifier.Notify(student);

        Console.WriteLine("\n=== GOOD DESIGN ===");
        List<IMessageService> services = new List<IMessageService>
        {
            new EmailMessageService(),
            new SMSMessageService()
        };
        
        GoodStudentNotifier goodNotifier = new GoodStudentNotifier(services);
        goodNotifier.Notify(student);
    }
}
