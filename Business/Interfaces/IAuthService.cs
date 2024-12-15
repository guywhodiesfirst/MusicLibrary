using Business.Models.Auth;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task Register(RegistrationRequestDto request);
    }
}