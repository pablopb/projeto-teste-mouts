namespace Mouts.DeveloperTest.Domain.DomainEvents
{
    public sealed record SaleCreatedEvent(int SaleId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }

    public sealed record SaleCancelledEvent(int SaleId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }

    public sealed record ItemCancelledEvent(int SaleId, int ProductId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
