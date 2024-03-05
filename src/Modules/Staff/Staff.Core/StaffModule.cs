﻿using BuildingBlocks.Abstractions.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Staff.Core.Features.SignUpReceptionist;
using Staff.Core.Persistence;
using Staff.Core.Security;

namespace Staff.Core;

public class StaffModule : IModule
{
    public IEndpointRouteBuilder ExposeEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.SignUpReceptionist();

        return endpoints;
    }

    public IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSecurity()
            .AddStaffPersistence(configuration);
    }
}
