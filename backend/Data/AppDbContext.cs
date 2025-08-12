using Microsoft.EntityFrameworkCore;
using RealEstateApi.Models;

namespace RealEstateApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Favorite>().HasKey(f => new { f.UserId, f.PropertyId });

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Property)
                .WithMany(p => p.FavoritedBy)
                .HasForeignKey(f => f.PropertyId);

            //Seed data for Properties
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = 1,
                    Title = "Modern City Apartment",
                    Address = "123 Main St, Downtown",
                    Price = 150000,
                    ListingType = "For Sale",
                    Bedrooms = 2,
                    Bathrooms = 2,
                    CarSpots = 1,
                    Description = "Stylish apartment in the city center with modern amenities.",
                    ImageUrlsJson = "[\"/images/apartment1.jpg\",\"/images/apartment2.jpg\"]"
                },
                new Property
                {
                    Id = 2,
                    Title = "Cozy Family Home",
                    Address = "45 Maple Drive, Suburbia",
                    Price = 250000,
                    ListingType = "For Sale",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    CarSpots = 2,
                    Description = "Comfortable family home with a big backyard.",
                    ImageUrlsJson = "[\"/images/house1.jpg\",\"/images/house2.jpg\"]"
                },
                new Property
                {
                    Id = 3,
                    Title = "Beachfront Villa",
                    Address = "78 Ocean View Road, Seaside",
                    Price = 750000,
                    ListingType = "For Sale",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    CarSpots = 2,
                    Description = "Luxury villa with direct beach access and ocean views.",
                    ImageUrlsJson = "[\"/images/villa1.jpg\",\"/images/villa2.jpg\"]"
                }
            );
        }
    }
}
