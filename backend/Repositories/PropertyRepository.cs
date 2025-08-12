using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.Text.Json;

namespace RealEstateApi.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _db;
        public PropertyRepository(AppDbContext db) => _db = db;

        public Task<List<Property>> GetAllAsync() => _db.Properties.ToListAsync();

        public Task<Property> GetByIdAsync(int id) => _db.Properties.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Property>> SearchAsync(decimal? minPrice, decimal? maxPrice, int? bedrooms, string? suburb, string? listingType)
        {
            var q = _db.Properties.AsQueryable();
            if (minPrice.HasValue) q = q.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue) q = q.Where(p => p.Price <= maxPrice.Value);
            if (bedrooms.HasValue) q = q.Where(p => p.Bedrooms == bedrooms.Value);
            if (!string.IsNullOrWhiteSpace(suburb)) q = q.Where(p => p.Address.Contains(suburb));
            if (!string.IsNullOrWhiteSpace(listingType)) q = q.Where(p => p.ListingType == listingType);
            return await q.ToListAsync();
        }

        public async Task SeedIfEmptyAsync()
        {
            if (await _db.Properties.AnyAsync()) return;

            var props = new List<Property>
            {
                new Property { Title = "Sunny Family Home", Address = "Greenwich, Sydney", Price = 950000, ListingType = "Sale", Bedrooms = 4, Bathrooms = 2, CarSpots = 2, Description = "Lovely family house", ImageUrlsJson = JsonSerializer.Serialize(new[]{"https://picsum.photos/seed/1/800/600"}) },
                new Property { Title = "Modern Apartment", Address = "CBD, Sydney", Price = 650000, ListingType = "Sale", Bedrooms = 2, Bathrooms = 1, CarSpots = 1, Description = "City apartment", ImageUrlsJson = JsonSerializer.Serialize(new[]{"https://picsum.photos/seed/2/800/600"}) },
                new Property { Title = "Cozy Studio", Address = "Newtown, Sydney", Price = 350, ListingType = "Rent", Bedrooms = 0, Bathrooms = 1, CarSpots = 0, Description = "Studio for rent", ImageUrlsJson = JsonSerializer.Serialize(new[]{"https://picsum.photos/seed/3/800/600"}) }
            };

            await _db.Properties.AddRangeAsync(props);
            await _db.SaveChangesAsync();
        }

        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
