using BuildingBlocks.Events.Basics;

namespace Staff.Core.Domain.Events;
internal record ReceptionistCreated(Guid Id, string FullName, string Email) : DomainEventBase;
