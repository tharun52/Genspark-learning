using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singleton
{
    class FileManager
    {
        private static FileManager instance = null;
        private StreamReader reader;
        private StreamWriter writer;
        private string filePath;

        private FileManager(string path)
        {
            filePath = path;
            reader = new StreamReader(filePath);
            writer = new StreamWriter(filePath, append: true);
        }

        public static FileManager GetInstance(string path)
        {
            if (instance == null)
            {
                instance = new FileManager(path);
            }
            return instance;
        }
        public void ReadFile()
        {
            string line;
            if (reader.ReadLine() == null)
            {
                System.Console.WriteLine("You read a empty file");
                return;
            }
            while ((line = reader.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
            }
        }
        public void WriteFile()
        {
            System.Console.WriteLine("Enter teh content to write (blank to stop) : ");
            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                writer.WriteLine(line);
            }
        }
        public void Close()
        {
            reader.Close();
            writer.Close();
        }
    }
}

