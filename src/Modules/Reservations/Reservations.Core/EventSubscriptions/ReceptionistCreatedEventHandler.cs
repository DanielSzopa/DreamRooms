using MassTransit;
using Microsoft.Extensions.Logging;
using Reservations.Core.Domain.Entities;
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
        var message = context.Message;
        var receptionist = Receptionist.Create(message.Id, message.FullName, message.Email);

        _logger.LogInformation($"Receptionist created consumed {receptionist.EmployeeId} {receptionist.FullName} {receptionist.Email} {context.CorrelationId}");
        return Task.CompletedTask;
    }
}
