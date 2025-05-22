public class BadGradeCalculator
{
    public string CalculateGrade(Student student, string gradingType)
    {
        if (gradingType == "Regular")
        {
            if (student.Score >= 90) return "A";
            else if (student.Score >= 80) return "B";
            else return "F";
        }
        else if (gradingType == "PassFail")
        {
            return student.Score >= 60 ? "Pass" : "Fail";
        }

        return "No Grade";
    }
}
