using BuildingBlocks.Messaging.Outbox.Processors;
using BuildingBlocks.Modules;
using Quartz;

namespace Staff.Core.Outbox;

[DisallowConcurrentExecution]
public class StaffOutBoxDomainEventNotificationsJob : IJob
{
    private readonly IDomainEventNotificationOutBoxProcessor _outBoxProcessor;

    public StaffOutBoxDomainEventNotificationsJob(IDomainEventNotificationOutBoxProcessor processor)
    {
        _outBoxProcessor = processor;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _outBoxProcessor.ProcessAsync(typeof(StaffOutBoxDomainEventNotificationsJob).GetModuleName(), context.CancellationToken);
    }
}
