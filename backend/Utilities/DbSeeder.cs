using RealEstateApi.Data;

namespace RealEstateApi
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            db.Database.EnsureCreated();
            var propRepo = new RealEstateApi.Repositories.PropertyRepository(db);
            propRepo.SeedIfEmptyAsync().GetAwaiter().GetResult();
        }
    }
}
