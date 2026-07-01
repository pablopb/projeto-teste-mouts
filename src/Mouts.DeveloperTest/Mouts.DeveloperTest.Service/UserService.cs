using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.InfraStructure;
using Mouts.DeveloperTest.InfraStructure.Authentication;
using Mouts.DeveloperTest.Shared.Dtos.User;
using Mouts.DeveloperTest.Domain.Enums;
using Mouts.DeveloperTest.Shared.Mappers;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.Service
{
    public sealed class UserService(IUserRepository repository, IPasswordHasher passwordHasher) : IUserService
    {
        public async Task<CreateUserResponse> Create(CreateUserRequest request)
        {
            if (await repository.ExistsByUserName(request.Username))
                throw new InvalidOperationException("Username already exists.");

            if (await repository.ExistsByEmail(request.Email))
                throw new InvalidOperationException("Email already exists.");

            var user = new User
            {
                UserId = new UserId(0),
                Email = new Email(request.Email),
                UserName = request.Username,
                PasswordHash = passwordHasher.Hash(request.Password),

                Name = new PersonName(
                    request.Name.Firstname,
                    request.Name.Lastname),

                Address = new Address(
                    request.Address.City,
                    request.Address.Street,
                    request.Address.Number,
                    request.Address.Zipcode,

                    new GeoLocation(
                        request.Address.Geolocation.Lat,
                        request.Address.Geolocation.Long)),

                Phone = new Phone(request.Phone),

                Status = request.Status,

                Role = request.Role
            };

            await repository.Add(user);

            return new CreateUserResponse
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

        public async Task<UserResponse> Delete(int id)
        {
            var user = await repository.GetById(new UserId(id));

            if (user is null)
                throw new KeyNotFoundException();

            await repository.Delete(user);

            return user.ToResponse();
        }

        public async Task<PagedResult<UserResponse>> GetAll(
    int page,
    int size,
    string? order)
        {
            var result = await repository.GetAll(
                page,
                size,
                order);

            return new PagedResult<UserResponse>
            {
                Data = result.Data
                    .Select(x => x.ToResponse())
                    .ToList(),

                CurrentPage = result.CurrentPage,

                TotalItems = result.TotalItems,

                TotalPages = result.TotalPages
            };
        }

        public async Task<UserResponse?> GetById(int id)
        {
            var user = await repository.GetById(new UserId(id));

            if (user is null)
                return null;

            return user.ToResponse();
        }

        public async Task<UserResponse> Update(
     int id,
     UpdateUserRequest request)
        {
            var user = await repository.GetById(new UserId(id));

            if (user is null)
                throw new KeyNotFoundException();

            user = user with
            {
                Email = new Email(request.Email),

                UserName = request.Username,

                PasswordHash = passwordHasher.Hash(request.Password),

                Name = new PersonName(
                    request.Name.Firstname,
                    request.Name.Lastname),

                Address = new Address(
                    request.Address.City,
                    request.Address.Street,
                    request.Address.Number,
                    request.Address.Zipcode,
                    new GeoLocation(
                        request.Address.Geolocation.Lat,
                        request.Address.Geolocation.Long)),

                Phone = new Phone(request.Phone),

                Status = request.Status,

                Role = request.Role
            };

            await repository.Update(user);

            return user.ToResponse();
        }
    }
}
