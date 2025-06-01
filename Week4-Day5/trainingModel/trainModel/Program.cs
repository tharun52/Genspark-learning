using System.Net.Http.Headers;
using System.Text;
using CsvHelper;
using System.Globalization;
using Newtonsoft.Json;

class Faq
{
    public string? question { get; set; }
    public string? answer { get; set; }
}

class Program
{
    static readonly string apiKey = "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";
    static readonly string model = "llama3-8b-8192"; // or llama3-70b-8192
    static readonly string apiUrl = "https://api.groq.com/openai/v1/chat/completions";

    static async Task Main(string[] args)
    {
        var faqs = LoadCsv("bank_faq.csv");

        Console.WriteLine("💬 Welcome to KKT Bank FAQ Bot. Type 'exit' to quit.\n");

        while (true)
        {
            Console.Write("You: ");
            var userInput = Console.ReadLine();

            if (userInput?.ToLower() == "exit")
                break;

            var systemPrompt = "You are a helpful banking assistant at KKT Bank in Chennai. Reply formally and clearly using the FAQ below when relevant.";

            var faqContext = string.Join("\n", faqs.Select(f => $"Q: {f.question}\nA: {f.answer}"));

            var messages = new[]
            {
                new { role = "system", content = systemPrompt + "\n\n" + faqContext },
                new { role = "user", content = userInput }
            };

            var requestBody = new
            {
                model = model,
                messages = messages,
                temperature = 0.2
            };

            var response = await SendChatRequestAsync(requestBody);
            Console.WriteLine($"\n🤖 Bot: {response}\n");
        }
    }

    static List<Faq> LoadCsv(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Faq>().ToList();
    }

    static async Task<string> SendChatRequestAsync(object requestBody)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var json = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        var responseJson = await response.Content.ReadAsStringAsync();

        Console.WriteLine("\n🔍 Raw Response from Groq API:\n" + responseJson + "\n");

        try
        {
            dynamic result = JsonConvert.DeserializeObject(responseJson);
            return result?.choices?[0]?.message?.content?.ToString() ?? "Sorry, I couldn't extract the response.";
        }
        catch (Exception ex)
        {
            return $"⚠️ Error parsing response: {ex.Message}";
        }
    }
}
