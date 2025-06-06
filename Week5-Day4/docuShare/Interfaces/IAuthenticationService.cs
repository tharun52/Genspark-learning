using docuShare.Models.DTOs;

namespace docuShare.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserLoginResponse> Login(UserLoginRequest user);
    }
}