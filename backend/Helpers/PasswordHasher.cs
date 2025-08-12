namespace RealEstateApi.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly Microsoft.AspNetCore.Identity.PasswordHasher<object> _ph = new();
        public string Hash(string password) => _ph.HashPassword(null, password);
        public bool Verify(string hash, string password) => _ph.VerifyHashedPassword(null, hash, password) != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;
    }
}
