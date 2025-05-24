using System;
using System.IO;

interface IFile
{
    void Read();
}

class File : IFile
{
    public string FileName { get; set; }

    public File(string fileName)
    {
        FileName = fileName;
    }

    public void Read()
    {
        if (System.IO.File.Exists(FileName))
        {
            string content = System.IO.File.ReadAllText(FileName);
            Console.WriteLine("[Access Granted] Reading sensitive file content...");
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("No file found.");
        }
    }
}

class User
{
    public string Username { get; set; }
    public string Role { get; set; }

    public User(string username, string role)
    {
        Username = username;
        Role = role;
    }

    public string GetRole()
    {
        return Role;
    }
}

// This is the proxy file
class ProxyFile : IFile
{
    private File realFile;
    private User currentUser;
    private string fileName;

    public ProxyFile(string fileName, User user)
    {
        this.fileName = fileName;
        realFile = new File(fileName);
        currentUser = user;
    }

    public void Read()
    {
        string role = currentUser.GetRole();
        if (role == "Admin")
        {
            realFile.Read();
        }
        else if (role == "User")
        {
            Console.WriteLine("[Limited Access] File Name: " + fileName);
        }
        else
        {
            Console.WriteLine("[Access Denied] You do not have permission to read this file.");
        }
    }
}