using Microsoft.Extensions.Logging;
using Mouts.DeveloperTest.Domain.DomainEvents;

namespace Mouts.DeveloperTest.Service.EventNotification
{
    public sealed class LoggingEventPublisher : IEventPublisher
    {
        private readonly ILogger<LoggingEventPublisher> _logger;

        public LoggingEventPublisher(
            ILogger<LoggingEventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                _logger.LogInformation(
                    "Domain Event: {EventName}",
                    @event.GetType().Name);
            }

            return Task.CompletedTask;
        }
    }
}
