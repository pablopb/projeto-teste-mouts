using Mouts.DeveloperTest.Domain.Enums;
using System.Net;

namespace Mouts.DeveloperTest.Domain
{
    public sealed record class User
    {
        public UserId UserId { get; init; }

        public Email Email { get; init; }

        public string UserName { get; init; } = string.Empty;

        public PasswordHash PasswordHash { get; init; }

        public PersonName Name { get; init; }

        public Address Address { get; init; }

        public Phone Phone { get; init; }

        public UserStatus Status { get; init; }

        public UserRole Role { get; init; }
    }

    public record struct UserId(int Value);
    public readonly record struct Email(string Value);
    public readonly record struct Phone(string Value);
    public sealed record PersonName(
    string FirstName,
    string LastName);

    public sealed record GeoLocation(
    string Latitude,
    string Longitude);

    public sealed record Address(
    string City,
    string Street,
    int Number,
    string ZipCode,
    GeoLocation GeoLocation);


}
