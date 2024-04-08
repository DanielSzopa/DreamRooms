using BuildingBlocks.Events.Basics;

namespace Staff.Contracts;
public record ReceptionistCreatedIntegrationEvent(string FullName, string Email) : IntegrationEventBase;
