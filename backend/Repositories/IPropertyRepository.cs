using RealEstateApi.Models;
namespace RealEstateApi.Repositories
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        Task<List<Property>> SearchAsync(decimal? minPrice, decimal? maxPrice, int? bedrooms, string suburb, string listingType);
        Task SeedIfEmptyAsync();
        Task SaveChangesAsync();
    }
}
