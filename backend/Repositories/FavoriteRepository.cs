using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data;
using RealEstateApi.Models;

namespace RealEstateApi.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _db;
        public FavoriteRepository(AppDbContext db) => _db = db;

        public Task AddAsync(Favorite fav)
        {
            _db.Favorites.Add(fav);
            return Task.CompletedTask;
        }

        public Task<Favorite> GetAsync(Guid userId, int propertyId)
            => _db.Favorites.Include(f => f.Property).FirstOrDefaultAsync(f => f.UserId == userId && f.PropertyId == propertyId);

        public Task<List<Favorite>> GetByUserAsync(Guid userId)
            => _db.Favorites.Include(f => f.Property).Where(f => f.UserId == userId).ToListAsync();

        public Task RemoveAsync(Favorite fav)
        {
            _db.Favorites.Remove(fav);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
