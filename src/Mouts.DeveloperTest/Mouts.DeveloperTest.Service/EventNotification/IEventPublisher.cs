using Mouts.DeveloperTest.Domain.DomainEvents;

namespace Mouts.DeveloperTest.Service.EventNotification
{
    public interface IEventPublisher
    {
        Task PublishAsync(IEnumerable<IDomainEvent> events);
    }
}
