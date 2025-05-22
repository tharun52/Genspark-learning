using System;

public class BadStudent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }

    public BadStudent(int id, string name, string email)
    {
        Id = id;
        Name = name;
        EmailAddress = email;
    }

    public void Save()
    {
        Console.WriteLine($"Student Saved: {ToString()}");
    }

    public void SendEmail(string subject, string body)
    {
        Console.WriteLine("\n--- Email Sent ---");
        Console.WriteLine($"To: {EmailAddress}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
    }

    public void Enrol(string course)
    {
        Console.WriteLine($"{Name} enrolled in the course: {course}");
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}";
    }
}
