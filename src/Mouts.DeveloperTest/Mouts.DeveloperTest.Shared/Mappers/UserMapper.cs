using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Shared.Dtos.User;

namespace Mouts.DeveloperTest.Shared.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.UserId.Value,

                Email = user.Email.Value,

                Username = user.UserName,

                Name = new(
                   user.Name.FirstName,
                   user.Name.LastName),

                Address = new(
                   user.Address.City,
                   user.Address.Street,
                   user.Address.Number,
                   user.Address.ZipCode,

                   new(
                       user.Address.GeoLocation.Latitude,
                       user.Address.GeoLocation.Longitude)),

                Phone = user.Phone.Value,

                Status = user.Status,

                Role = user.Role
            };
        }
    }
}
