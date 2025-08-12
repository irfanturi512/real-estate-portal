using RealEstateApi.DTOs;
using RealEstateApi.Models;
using RealEstateApi.Repositories;
using RealEstateApi.Helpers;

namespace RealEstateApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepo, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var exists = await _userRepo.GetByEmailAsync(dto.Email);
            if (exists != null) throw new Exception("User already exists");

            var user = new User { Email = dto.Email, PasswordHash = _passwordHasher.Hash(dto.Password) };
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null) throw new Exception("Invalid credentials");
            if (!_passwordHasher.Verify(user.PasswordHash, dto.Password)) throw new Exception("Invalid credentials");

            return _jwtService.GenerateToken(user);
        }
    }
}
