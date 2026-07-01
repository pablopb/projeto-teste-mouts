namespace Mouts.DeveloperTest.Shared.Pagination
{
    public sealed record PagedResult<T>
    {
        public IReadOnlyCollection<T> Data { get; init; } = [];

        public int TotalItems { get; init; }

        public int CurrentPage { get; init; }

        public int TotalPages { get; init; }
    }
}
