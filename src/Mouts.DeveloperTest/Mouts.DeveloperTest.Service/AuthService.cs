using Mouts.DeveloperTest.InfraStructure;
using Mouts.DeveloperTest.InfraStructure.Authentication;

namespace Mouts.DeveloperTest.Service
{
    public sealed class AuthService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher) : IAuthService
    {
        public async Task<string> Authenticate(string userName, string password)
        {
            var user = await userRepository.GetByUserName(userName);

            if (user is null)
                throw new UnauthorizedAccessException();

            if (!passwordHasher.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException();

            return tokenGenerator.Generate(user);
        }
    }
}
