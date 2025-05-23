class Program
{
    static void Main(string[] args)
    {
        User admin = new User("Alice", "Admin");
        User guest = new User("Bob", "Guest");
        User user = new User("Charlie", "User");

        IFile file1 = new ProxyFile("example.txt", admin);
        file1.Read();

        IFile file2 = new ProxyFile("example.txt", user);
        file2.Read();

        IFile file3 = new ProxyFile("example.txt", guest);
        file3.Read();
    }
}
