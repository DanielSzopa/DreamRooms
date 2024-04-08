namespace BuildingBlocks.Events.Basics;
public interface IIntegrationEvent
{
    Guid EventId { get; }
}
