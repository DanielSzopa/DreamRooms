namespace BuildingBlocks.Events.Publishers;
internal interface IDomainEventNotificationsPublisher
{
    IEnumerable<Task> PublishAsync(string notificationType, string notifiticationData, CancellationToken cancellationToken = default);
}
