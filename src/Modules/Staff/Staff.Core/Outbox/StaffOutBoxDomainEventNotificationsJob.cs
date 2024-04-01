using BuildingBlocks.Commands;
using Quartz;

namespace Staff.Core.Outbox;

[DisallowConcurrentExecution]
public class StaffOutBoxDomainEventNotificationsJob : IJob
{
    private readonly ICommandDispatcher _commandDispatcher;

    public StaffOutBoxDomainEventNotificationsJob(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var command = new StaffOutboxDomainEventNotificationCommand();
        await _commandDispatcher.SendAsync(command, context.CancellationToken);
    }
}
