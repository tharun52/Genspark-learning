using System;

class Program
{
    static void Main()
    {
        Student student1 = new Student("John", 75);
        Student student2 = new Student("Alice", 85);

        Console.WriteLine(student1);  
        Console.WriteLine(student2);

        // Violate Open Close
        BadGradeCalculator badCalc = new BadGradeCalculator();
        Console.WriteLine($"BadGradeCalculator (Regular): {badCalc.CalculateGrade(student1, "Regular")}");
        Console.WriteLine($"BadGradeCalculator (PassFail): {badCalc.CalculateGrade(student1, "PassFail")}");

        // Follows Open Close
        GoodGradeCalculator goodCalc = new GoodGradeCalculator(new RegularGrading());
        Console.WriteLine($"GoodGradeCalculator (Regular): {goodCalc.CalculateGrade(student2)}");

        goodCalc = new GoodGradeCalculator(new PassFailGrading());
        Console.WriteLine($"GoodGradeCalculator (PassFail): {goodCalc.CalculateGrade(student2)}");
    }
}
