public class Teacher
{
    public virtual void Teach()
    {
        Console.WriteLine("Teaching students.");
    }
}

public class Administrator : Teacher
{
    public override void Teach()
    {
        // Here Administrator is not supposed to teach, so we throw an exception.
        // This violates the Liskov Substitution Principle.
        throw new NotImplementedException("Administrators don't teach.");
    }
}