public interface IGradingStrategy
{
    string CalculateGrade(Student student);
}

public class RegularGrading : IGradingStrategy
{
    public string CalculateGrade(Student student)
    {
        if (student.Score >= 90) return "A";
        else if (student.Score >= 80) return "B";
        else return "F";
    }
}

public class PassFailGrading : IGradingStrategy
{
    public string CalculateGrade(Student student)
    {
        return student.Score >= 60 ? "Pass" : "Fail";
    }
}

public class GoodGradeCalculator
{
    private readonly IGradingStrategy _gradingStrategy;

    public GoodGradeCalculator(IGradingStrategy gradingStrategy)
    {
        _gradingStrategy = gradingStrategy;
    }

    public string CalculateGrade(Student student)
    {
        return _gradingStrategy.CalculateGrade(student);
    }
}
