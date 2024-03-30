using BuildingBlocks.Events.DomainEventsHandlers;
using Microsoft.Extensions.Logging;
using Staff.Core.Domain.Events;

namespace Staff.Core.Domain.Events.EventHandlers;

internal class SendReceptionistCreatedEmailDomainEventHandler : IDomainEventHandler<ReceptionistCreated>
{
    private readonly ILogger<SendReceptionistCreatedEmailDomainEventHandler> _logger;

    public SendReceptionistCreatedEmailDomainEventHandler(ILogger<SendReceptionistCreatedEmailDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ReceptionistCreated @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Send email to {email}, Welcome our new receptionist....", @event.Email);
        return Task.CompletedTask;
    }
}
