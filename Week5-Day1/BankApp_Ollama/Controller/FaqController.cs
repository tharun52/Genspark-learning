using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using System.Text;
using BankApp.Repositories;
using BankApp.Models;
using BankApp.Interfaces;
using Microsoft.Extensions.AI;

namespace BankApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly IChatHistoryRepository _chatHistoryRepo;
        private readonly IFAQRepository        _faqRepo;
        private readonly IChatClient           _ollamaClient;

        public FaqController(
            IChatHistoryRepository chatHistoryRepo,
            IFAQRepository        faqRepo)
        {
            _chatHistoryRepo = chatHistoryRepo;
            _faqRepo         = faqRepo;

            _ollamaClient    = new OllamaApiClient(new Uri("http://localhost:11434/"), "phi3:mini");

            if (!_chatHistoryRepo.GetHistory().Messages.Any())
            {
                _chatHistoryRepo.AddMessage(new ChatEntry
                {
                    Role = "system",
                    Text = "You are a helpful assistant for KKT Bank in Chennai. Use the FAQ below as your context."
                });

                var allFaqs = _faqRepo.GetAll()
                    .Select(f => $"Q: {f.Question}\nA: {f.Answer} (class: {f.Class})")
                    .ToList();

                var faqContext = new StringBuilder("FAQ List:\n");
                foreach (var line in allFaqs)
                {
                    faqContext.AppendLine(line);
                }

                _chatHistoryRepo.AddMessage(new ChatEntry
                {
                    Role = "system",
                    Text = faqContext.ToString()
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                return BadRequest(new { error = "Please provide a non‐empty 'question' query parameter." });

            _chatHistoryRepo.AddMessage(new ChatEntry
            {
                Role = "user",
                Text = question
            });

            var ollamaMessages = _chatHistoryRepo.GetHistory()
                .Messages
                .Select(m =>
                    m.Role switch
                    {
                        "system"    => new ChatMessage(ChatRole.System, m.Text),
                        "user"      => new ChatMessage(ChatRole.User, m.Text),
                        "assistant" => new ChatMessage(ChatRole.Assistant, m.Text),
                        _           => new ChatMessage(ChatRole.User, m.Text)
                    })
                .ToList();

            var sb = new StringBuilder();
            await foreach (var chunk in _ollamaClient.GetStreamingResponseAsync(ollamaMessages))
            {
                sb.Append(chunk.Text);
            }
            var assistantReply = sb.ToString();

            _chatHistoryRepo.AddMessage(new ChatEntry
            {
                Role = "assistant",
                Text = assistantReply
            });

            return Ok(_chatHistoryRepo.GetHistory());
        }

        [HttpDelete("clear")]
        public IActionResult ClearChat()
        {
            _chatHistoryRepo.Clear();
            return NoContent();
        }
    }
}
