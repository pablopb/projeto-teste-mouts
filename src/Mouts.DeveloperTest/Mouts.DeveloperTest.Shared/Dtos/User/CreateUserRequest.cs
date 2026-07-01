using Mouts.DeveloperTest.Domain.Enums;

namespace Mouts.DeveloperTest.Shared.Dtos.User
{
    public sealed record CreateUserRequest
    {
        public string Email { get; init; } = string.Empty;

        public string Username { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public CreateUserNameRequest Name { get; init; } = default!;

        public CreateUserAddressRequest Address { get; init; } = default!;

        public string Phone { get; init; } = string.Empty;

        public UserStatus Status { get; init; }

        public UserRole Role { get; init; }
    }

}
