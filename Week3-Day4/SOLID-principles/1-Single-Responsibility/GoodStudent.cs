public class GoodStudent
{
    public int Id { get; }
    public string Name { get; }
    public string EmailAddress { get; }

    public GoodStudent(int id, string name, string email)
    {
        Id = id;
        Name = name;
        EmailAddress = email;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}";
    }
}
