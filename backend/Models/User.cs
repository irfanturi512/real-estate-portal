using System.ComponentModel.DataAnnotations;
namespace RealEstateApi.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public List<Favorite> Favorites { get; set; } = new();
    }
}
