using BuildingBlocks.Commands;
using BuildingBlocks.Messaging.Outbox.Commands;
using BuildingBlocks.Modules;
using Quartz;

namespace BuildingBlocks.Messaging.Outbox.Jobs;

[DisallowConcurrentExecution]
internal class OutBoxDomainEventNotificationsJob<TModule> : IJob
    where TModule : class, IModule
{
    private static readonly string ModuleName = typeof(TModule).GetModuleName();
    internal static readonly JobKey Key = new JobKey($"{ModuleName}-OutBox-DomainEventNotifications-Job", "OutBox-DomainEvenNotifications-Group");
    private readonly ICommandDispatcher _commandDispatcher;

    public OutBoxDomainEventNotificationsJob(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var command = new DomainEventNotificationOutBoxCommand(typeof(TModule).GetModuleName());
        await _commandDispatcher.SendAsync(command, context.CancellationToken);
    }
}
