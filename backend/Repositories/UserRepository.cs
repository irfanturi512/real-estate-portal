using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data;
using RealEstateApi.Models;

namespace RealEstateApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public Task<User> GetByEmailAsync(string email)
            => _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task<User> GetByIdAsync(Guid id)
            => _db.Users.Include(u => u.Favorites).ThenInclude(f => f.Property).FirstOrDefaultAsync(u => u.Id == id);

        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
