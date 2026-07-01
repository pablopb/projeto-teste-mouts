using Mouts.DeveloperTest.Domain;

namespace Mouts.DeveloperTest.InfraStructure.Authentication
{
    public interface IPasswordHasher
    {
        PasswordHash Hash(string password);
        bool Verify(string password, PasswordHash hash);
    }
}
