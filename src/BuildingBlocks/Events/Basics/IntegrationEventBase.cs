namespace BuildingBlocks.Events.Basics;
public abstract record IntegrationEventBase : IIntegrationEvent
{
    public Guid EventId => Guid.NewGuid();
}
