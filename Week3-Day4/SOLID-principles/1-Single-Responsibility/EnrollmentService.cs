using System;

public class EnrollmentService
{
    public void Enroll(GoodStudent student, string course)
    {
        Console.WriteLine($"{student.Name} enrolled in the course: {course}");
    }
}
