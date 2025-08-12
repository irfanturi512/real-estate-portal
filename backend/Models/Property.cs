using System.ComponentModel.DataAnnotations;
namespace RealEstateApi.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string ListingType { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int CarSpots { get; set; }
        public string Description { get; set; }
        public string ImageUrlsJson { get; set; }

        public List<Favorite> FavoritedBy { get; set; } = new();
    }
}
