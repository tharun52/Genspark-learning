using System.Net.Http.Headers;
using System.Text;
using BankApp.Interfaces;
using Newtonsoft.Json;

namespace BankApp.Services
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepository;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model = "llama3-8b-8192";
        private readonly string _apiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public FaqService(IFaqRepository faqRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _faqRepository = faqRepository;
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["GROQ_API_KEY"] ?? throw new ArgumentNullException("API key not configured in environment variables.");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetAnswerAsync(string userInput)
        {
            var faqs = await _faqRepository.GetFaqsAsync();
            var relevantFaqs = faqs
                .Where(f => userInput.Contains(f.Question, StringComparison.OrdinalIgnoreCase) ||
                            f.Question.Split(' ').Any(word => userInput.Contains(word, StringComparison.OrdinalIgnoreCase)))
                .Take(20) 
                .ToList();

            var systemPrompt = "You are a helpful banking assistant at KKT Bank in Chennai. Reply formally and clearly using the FAQ below when relevant.";
            var faqContext = string.Join("\n", relevantFaqs.Select(f => $"Q: {f.Question}\nA: {f.Answer}"));

            var messages = new[]
            {
                new { role = "system", content = systemPrompt + "\n\n" + faqContext },
                new { role = "user", content = userInput }
            };

            var requestBody = new
            {
                model = _model,
                messages = messages,
                temperature = 0.2
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

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
}
