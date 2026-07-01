using Mouts.DeveloperTest.Domain.Enums;

namespace Mouts.DeveloperTest.Shared.Dtos.User
{
    public sealed record UpdateUserRequest
    {
        public string Email { get; init; } = string.Empty;

        public string Username { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public UpdateUserNameRequest Name { get; init; } = default!;

        public UpdateUserAddressRequest Address { get; init; } = default!;

        public string Phone { get; init; } = string.Empty;

        public UserStatus Status { get; init; }

        public UserRole Role { get; init; }
    }

    public sealed record UpdateUserNameRequest(
        string Firstname,
        string Lastname);

    public sealed record UpdateUserAddressRequest(
        string City,
        string Street,
        int Number,
        string Zipcode,
        UpdateUserGeoLocationRequest Geolocation);

    public sealed record UpdateUserGeoLocationRequest(
        string Lat,
        string Long);
}
