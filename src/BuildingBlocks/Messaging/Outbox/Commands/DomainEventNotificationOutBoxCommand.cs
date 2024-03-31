using BuildingBlocks.Commands;

namespace BuildingBlocks.Messaging.Outbox.Commands
{
    internal record DomainEventNotificationOutBoxCommand(string Module) : INotificationOutBoxCommand;
}
