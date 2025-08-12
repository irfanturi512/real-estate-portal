using RealEstateApi.Models;
namespace RealEstateApi.Helpers
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
