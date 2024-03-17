﻿using BuildingBlocks.Events.NotificationsRegistery;
using Microsoft.Extensions.DependencyInjection;

namespace Staff.Core.Domain.Events;
internal static class Extensions
{
    internal static IServiceCollection RegisterStaffDomainEventNotifications(this IServiceCollection services)
    {
        return services
            .RegisterDomainEventNotification<ReceptionistCreated, ReceptionistCreatedNotification>();
    }
}
