using Microsoft.AspNetCore.Identity;
using Mouts.DeveloperTest.Domain;

namespace Mouts.DeveloperTest.InfraStructure.Authentication
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private readonly Microsoft.AspNetCore.Identity.PasswordHasher<object> _passwordHasher =
      new();

        public PasswordHash Hash(string password)
        {
            var hash = _passwordHasher.HashPassword(null!, password);

            return new PasswordHash(hash);
        }

        public bool Verify(string password, PasswordHash hash)
        {
            var result = _passwordHasher.VerifyHashedPassword(
           null!,
           hash.Value,
           password);

            return result is PasswordVerificationResult.Success
                or PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
