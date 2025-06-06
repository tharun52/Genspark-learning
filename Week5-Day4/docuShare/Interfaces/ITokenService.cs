using docuShare.Models;

namespace docuShare.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}