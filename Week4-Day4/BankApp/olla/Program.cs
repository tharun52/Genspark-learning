using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using OllamaSharp;
using Microsoft.Extensions.AI;

// Define a simple structure to hold FAQ data
public class Faq
{
    public string Question { get; set; }
    public string Answer { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        var faqs = LoadFaqs("faq.csv");

        IChatClient chatClient =
            new OllamaApiClient(new Uri("http://localhost:11434/"), "phi3:mini");

        List<ChatMessage> chatHistory = new();

        // Add FAQs as system context
        string context = "You are a helpful assistant for KKT Bank in Chennai. Answer questions based on these FAQs:\n";
        foreach (var faq in faqs)
        {
            context += $"Q: {faq.Question}\nA: {faq.Answer}\n";
        }

        chatHistory.Add(new ChatMessage(ChatRole.System, context));

        // Run chat loop
        while (true)
        {
            Console.WriteLine("Your prompt:");
            var userPrompt = Console.ReadLine();
            chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

            Console.WriteLine("AI Response:");
            var response = "";
            await foreach (ChatResponseUpdate item in chatClient.GetStreamingResponseAsync(chatHistory))
            {
                Console.Write(item.Text);
                response += item.Text;
            }

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
            Console.WriteLine();
        }
    }

    // Function to load FAQs from CSV
    public static List<Faq> LoadFaqs(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Faq>();
        return new List<Faq>(records);
    }
}
