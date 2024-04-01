using BuildingBlocks.Abstractions.Commands;

namespace Staff.Core.Outbox
{
    internal class StaffOutBoxDomainEventNotificationCommandHandler : ICommandHandler<StaffOutboxDomainEventNotificationCommand>
    {
        public Task HandleAsync(StaffOutboxDomainEventNotificationCommand command, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
