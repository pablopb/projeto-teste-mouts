using Mouts.DeveloperTest.Domain.Enums;

namespace Mouts.DeveloperTest.Shared.Dtos.User
{
    public sealed record CreateUserResponse
    {
        public int Id { get; init; }

        public string Email { get; init; } = string.Empty;

        public string Username { get; init; } = string.Empty;

        public UserNameResponse Name { get; init; } = default!;

        public UserAddressResponse Address { get; init; } = default!;

        public string Phone { get; init; } = string.Empty;

        public UserStatus Status { get; init; }

        public UserRole Role { get; init; }
    }

    public sealed record UserNameResponse(
        string Firstname,
        string Lastname);

    public sealed record UserAddressResponse(
        string City,
        string Street,
        int Number,
        string Zipcode,
        UserGeoLocationResponse Geolocation);

    public sealed record UserGeoLocationResponse(
        string Lat,
        string Long);
}
