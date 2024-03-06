using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.UnitOfWork;

public static class Extensions
{
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services)
        where T : class, IUnitOfWork
    {
        services.AddScoped<T>();
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistery>()
            .Register<T>();

        return services;
    }
}
