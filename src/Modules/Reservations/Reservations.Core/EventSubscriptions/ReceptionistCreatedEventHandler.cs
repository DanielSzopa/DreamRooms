using MassTransit;
using Microsoft.Extensions.Logging;
using Staff.Contracts;

namespace Reservations.Core.EventSubscriptions;
public class ReceptionistCreatedEventHandler : IConsumer<ReceptionistCreatedIntegrationEvent>
{
    private readonly ILogger<ReceptionistCreatedEventHandler> _logger;

    public ReceptionistCreatedEventHandler(ILogger<ReceptionistCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ReceptionistCreatedIntegrationEvent> context)
    {
        _logger.LogInformation($"Receptionist created consumed {context.Message.Id} {context.Message.FullName} {context.Message.Email}");
        return Task.CompletedTask;
    }
}
