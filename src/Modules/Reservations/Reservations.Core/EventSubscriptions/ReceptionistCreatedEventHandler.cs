using MassTransit;
using Microsoft.Extensions.Logging;
using Staff.Contracts;

namespace Reservations.Core.EventSubscriptions;
public class ReceptionistCreatedEventHandler : IConsumer<ReceptionistCreated>
{
    private readonly ILogger<ReceptionistCreated> _logger;

    public ReceptionistCreatedEventHandler(ILogger<ReceptionistCreated> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ReceptionistCreated> context)
    {
        _logger.LogInformation($"Receptionist created consumed {context.Message.Id} {context.Message.FullName} {context.Message.Email}");
        return Task.CompletedTask;
    }
}
