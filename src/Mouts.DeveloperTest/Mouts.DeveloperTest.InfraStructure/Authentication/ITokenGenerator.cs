using Mouts.DeveloperTest.Domain;

namespace Mouts.DeveloperTest.InfraStructure.Authentication
{
    public interface ITokenGenerator
    {
        string Generate(User user);
    }
}
