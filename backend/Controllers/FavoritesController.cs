using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Repositories;
using RealEstateApi.Models;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("favorites")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteRepository _favRepo;
        private readonly IUserRepository _userRepo;

        public FavoritesController(IFavoriteRepository favRepo, IUserRepository userRepo)
        {
            _favRepo = favRepo;
            _userRepo = userRepo;
        }

        private Guid CurrentUserId
        {
            get
            {
                var sub = User.Claims.ToList()[0].Value  ;
                return Guid.Parse(sub);
            }
        }

        [HttpPost("{propertyId}")]
        public async Task<IActionResult> ToggleFavorite(int propertyId)
        {
            var userId = CurrentUserId;
            var existing = await _favRepo.GetAsync(userId, propertyId);
            if (existing != null)
            {
                await _favRepo.RemoveAsync(existing);
                await _favRepo.SaveChangesAsync();
                return Ok(new { removed = true });
            }

            var fav = new Favorite { UserId = userId, PropertyId = propertyId };
            await _favRepo.AddAsync(fav);
            await _favRepo.SaveChangesAsync();
            return Ok(new { added = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = CurrentUserId;
            var list = await _favRepo.GetByUserAsync(userId);
            var results = list.Select(f => new {
                f.Property.Id,
                f.Property.Title,
                f.Property.Address,
                f.Property.Price,
                f.Property.ListingType
            });
            return Ok(results);
        }
    }
}
