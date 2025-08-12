using RealEstateApi.DTOs;
namespace RealEstateApi.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
