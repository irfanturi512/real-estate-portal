namespace RealEstateApi.Models
{
    public class Favorite
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
