using BuildingBlocks.Abstractions.Commands;

namespace Staff.Core.Outbox;

internal record StaffOutboxDomainEventNotificationCommand() : ICommand;
