using RealEstateApi.Models;
namespace RealEstateApi.Repositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite> GetAsync(Guid userId, int propertyId);
        Task AddAsync(Favorite fav);
        Task RemoveAsync(Favorite fav);
        Task<List<Favorite>> GetByUserAsync(Guid userId);
        Task SaveChangesAsync();
    }
}
