using MyApiApp.Interfaces;

namespace MyApiApp.Services
{
    public class WelcomeService : IWelcomeService
    {
        public string GetLongWelcomeMessage()
        {
            return @"
Welcome!
Thank you for visiting this url
";
        }
    }
}