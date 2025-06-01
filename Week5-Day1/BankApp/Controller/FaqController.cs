using Microsoft.AspNetCore.Mvc;
using BankApp.Services;
using BankApp.Interfaces;

namespace BankApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskQuestion([FromBody] string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                return BadRequest("Question is required.");

            var answer = await _faqService.GetAnswerAsync(question);
            return Ok(new { answer });
        }
    }
}
