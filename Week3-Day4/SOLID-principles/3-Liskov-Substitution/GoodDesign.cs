public abstract class Employee
{
    public abstract void Work();
}

public class GoodTeacher : Employee
{
    public override void Work()
    {
        Teach();
    }

    private void Teach()
    {
        Console.WriteLine("Teaching students.");
    }
}

public class GoodAdministrator : Employee
{
    public override void Work()
    {
        Manage();
    }

    private void Manage()
    {
        Console.WriteLine("Managing school operations.");
    }
}