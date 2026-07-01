using Microsoft.EntityFrameworkCore;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Shared.Pagination;
using System.Linq.Expressions;

namespace Mouts.DeveloperTest.InfraStructure
{
    public sealed class UserRepository(DataContext dataContext) : IUserRepository
    {
        public async Task Add(User user)
        {
           dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmail(string email)
        => await dataContext.Users.AnyAsync(x => x.Email.Value ==  email);

        public async Task<bool> ExistsByUserName(string userName)
          => await dataContext.Users.AnyAsync(x => x.UserName == userName);

        public async Task<User?> GetByUserName(string username)
        {
            return await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<User?> GetById(UserId id)
        {
            return await dataContext.Users
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<PagedResult<User>> GetAll(
    int page,
    int size,
    string? order)
        {
            IQueryable<User> query = dataContext.Users;

            query = ApplyOrdering(query, order);

            var totalItems = await query.CountAsync();

            var users = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<User>
            {
                Data = users,
                CurrentPage = page,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }

        public Task Update(User user)
        {
            dataContext.Users.Update(user);

            return dataContext.SaveChangesAsync();
        }

        public Task Delete(User user)
        {
            dataContext.Users.Remove(user);

            return dataContext.SaveChangesAsync();
        }

        private static IQueryable<User> ApplyOrdering(
    IQueryable<User> query,
    string? order)
        {
            if (string.IsNullOrWhiteSpace(order))
                return query.OrderBy(x => x.UserName);

            IOrderedQueryable<User>? ordered = null;

            foreach (var item in order.Split(','))
            {
                var parts = item.Trim().Split(' ');

                var property = parts[0].ToLower();

                var desc = parts.Length > 1 &&
                           parts[1].Equals("desc",
                               StringComparison.OrdinalIgnoreCase);

                ordered = property switch
                {
                    "username" => Apply(
                        ordered,
                        query,
                        x => x.UserName,
                        desc),

                    "email" => Apply(
                        ordered,
                        query,
                        x => x.Email.Value,
                        desc),

                    "status" => Apply(
                        ordered,
                        query,
                        x => x.Status,
                        desc),

                    "role" => Apply(
                        ordered,
                        query,
                        x => x.Role,
                        desc),

                    _ => ordered
                };
            }

            return ordered ?? query;
        }

        private static IOrderedQueryable<User> Apply<TKey>(
    IOrderedQueryable<User>? ordered,
    IQueryable<User> query,
    Expression<Func<User, TKey>> selector,
    bool desc)
        {
            if (ordered is null)
                return desc
                    ? query.OrderByDescending(selector)
                    : query.OrderBy(selector);

            return desc
                ? ordered.ThenByDescending(selector)
                : ordered.ThenBy(selector);
        }
    }
}
