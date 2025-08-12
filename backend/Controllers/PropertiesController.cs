using Microsoft.AspNetCore.Mvc;
using RealEstateApi.DTOs;
using RealEstateApi.Repositories;
using System.Text.Json;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("properties")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _propRepo;

        public PropertiesController(IPropertyRepository propRepo)
        {
            _propRepo = propRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int? bedrooms, [FromQuery] string? suburb, [FromQuery] string? listingType)
        {
            var list = await _propRepo.SearchAsync(minPrice, maxPrice, bedrooms, suburb, listingType);
            var dtos = list.Select(p => new PropertyDto {
                Id = p.Id,
                Title = p.Title,
                Address = p.Address,
                Price = p.Price,
                ListingType = p.ListingType,
                Bedrooms = p.Bedrooms,
                Bathrooms = p.Bathrooms,
                CarSpots = p.CarSpots,
                Description = p.Description,
                ImageUrls = string.IsNullOrWhiteSpace(p.ImageUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(p.ImageUrlsJson)
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _propRepo.GetByIdAsync(id);
            if (p == null) return NotFound();
            var dto = new PropertyDto {
                Id = p.Id,
                Title = p.Title,
                Address = p.Address,
                Price = p.Price,
                ListingType = p.ListingType,
                Bedrooms = p.Bedrooms,
                Bathrooms = p.Bathrooms,
                CarSpots = p.CarSpots,
                Description = p.Description,
                ImageUrls = string.IsNullOrWhiteSpace(p.ImageUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(p.ImageUrlsJson)
            };
            return Ok(dto);
        }
    }
}
