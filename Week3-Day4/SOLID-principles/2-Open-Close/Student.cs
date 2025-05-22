public class Student
{
    public string Name { get; set; }
    public int Score { get; set; }

    public Student(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public override string ToString()
    {
        return $"Student(Name: {Name}, Score: {Score})";
    }
}
